using EnsureThat;
using FlowSynx.Parsers.Percent;
using Microsoft.Extensions.Logging;
using System.Data;

namespace FlowSynx.Data.Filter;

public class DataFilter : IDataFilter
{
    private readonly ILogger<DataFilter> _logger;
    private readonly IPercentParser _percentParser;

    public DataFilter(ILogger<DataFilter> logger, IPercentParser percentParser)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(percentParser, nameof(percentParser));
        _logger = logger;
        _percentParser = percentParser;
    }

    public DataTable Filter(DataTable dataTable, DataFilterOptions? dataFilterOptions)
    {
        if (dataFilterOptions == null) 
            return dataTable;

        if (dataFilterOptions.FilterExpression is null && dataFilterOptions.SortExpression is null)
            return dataTable;

        dataTable.CaseSensitive = dataFilterOptions.CaseSensetive.HasValue 
                                ? dataFilterOptions.CaseSensetive.Value 
                                : false;

        var view = dataTable.DefaultView;

        if (!string.IsNullOrEmpty(dataFilterOptions.FilterExpression))
            view.RowFilter = dataFilterOptions.FilterExpression;

        if (!string.IsNullOrWhiteSpace(dataFilterOptions.SortExpression))
            view.Sort = dataFilterOptions.SortExpression;

        var result = dataFilterOptions.Fields == null
                    ? view.ToTable(false) 
                    : view.ToTable(false, dataFilterOptions.Fields);

        var maxResult = _percentParser.Parse(dataFilterOptions.Limit, view.Count);
        if (maxResult > 0)
            result = result.AsEnumerable().Take(maxResult).CopyToDataTable();

        return result;
    }
}