using FlowSynx.Data.DataTableQuery.Fields;
using FlowSynx.Data.DataTableQuery.Filters;
using FlowSynx.Data.DataTableQuery.Pagination;
using FlowSynx.Data.DataTableQuery.Sorting;

namespace FlowSynx.Data.DataTableQuery.Queries.Select;

public class SelectDataTableOption
{
    public FieldsList? Fields { get; set; } = new();
    public FiltersList? Filters { get; set; } = new();
    public SortsList? Sorts { get; set; } = new();
    public Paging? Paging { get; set; } = new();
    public bool? CaseSensitive { get; set; } = false;
}
