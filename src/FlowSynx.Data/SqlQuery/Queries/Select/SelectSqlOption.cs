using FlowSynx.Data.SqlQuery.Fields;
using FlowSynx.Data.SqlQuery.Filters;
using FlowSynx.Data.SqlQuery.Grouping;
using FlowSynx.Data.SqlQuery.Joins;
using FlowSynx.Data.SqlQuery.Pagination;
using FlowSynx.Data.SqlQuery.Sorting;
using FlowSynx.Data.SqlQuery.Tables;

namespace FlowSynx.Data.SqlQuery.Queries.Select;

public class SelectSqlOption
{
    public required Table Table { get; set; }
    public FieldsList Fields { get; set; } = new();
    public JoinsList? Joins { get; set; } = new();
    public FiltersList? Filters { get; set; } = new();
    public GroupByList? GroupBy { get; set; } = new();
    public SortsList? Sorts { get; set; } = new();
    public Paging? Paging { get; set; } = new();
}
