using FlowSynx.Data.DataTableQuery.Fetches;
using FlowSynx.Data.DataTableQuery.Fields;
using FlowSynx.Data.DataTableQuery.Filters;
using FlowSynx.Data.DataTableQuery.Sorting;
using System.Data;

namespace FlowSynx.Data.DataTableQuery.Queries;

public class FilterDataTable
{
    private readonly DataTable _dataTable;

    public FieldsList Fields { get; set; }
    public FiltersList? Filters { get; set; }
    public SortsList? Sorts { get; set; }
    public Fetch? Fetch { get; set; }
    public bool? CaseSensitive { get; set; } = false;

    public FilterDataTable(DataTable dataTable)
    {
        Fields = new FieldsList();
        Filters = new FiltersList();
        Sorts = new SortsList();
        Fetch = new Fetch();
        _dataTable = dataTable;
    }

    public DataTable GetQuery()
    {
        _dataTable.CaseSensitive = CaseSensitive ?? false;
        var view = _dataTable.DefaultView;

        if (Filters is { Count: > 0 })
            view.RowFilter = Filters.GetQuery();

        if (Sorts is { Count: > 0 })
            view.Sort = Sorts.GetQuery();

        var result = Sorts is { Count: > 0 } 
            ? view.ToTable(false, Fields.GetQuery()) 
            : view.ToTable(false);

        if (Fetch is not null)
            result = result.AsEnumerable().Take(Fetch.GetQuery()).CopyToDataTable();

        return result;
    }
}
