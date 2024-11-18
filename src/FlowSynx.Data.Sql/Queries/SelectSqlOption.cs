using FlowSynx.Data.Sql;

namespace FlowSynx.Data.SqlQuery.Queries;

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
