using System.Data;
using FlowSynx.Data.DataTableQuery.Queries.Select;

namespace FlowSynx.Data.DataTableQuery.Queries;

public class DataTableService : IDataTableService
{
    public DataTable Select(DataTable dataTable, SelectDataTableOption option)
    {
        dataTable.CaseSensitive = option.CaseSensitive ?? false;
        var view = dataTable.DefaultView;

        if (option.Filters is { Count: > 0 })
            view.RowFilter = option.Filters.GetQuery();

        if (option.Sorts is { Count: > 0 })
            view.Sort = option.Sorts.GetQuery();

        var result = option.Sorts is { Count: > 0 }
            ? view.ToTable(false, option.Fields.GetQuery())
            : view.ToTable(false);

        if (option.Paging is not null)
            result = result.AsEnumerable().Take(option.Paging.GetQuery()).CopyToDataTable();

        return result;
    }
}