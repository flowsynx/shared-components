namespace FlowSynx.Data.Sql.Builder;

public class SelectOption
{
    public required Table Table { get; set; }
    public FieldsList Fields { get; set; } = new();
    public JoinList? Join { get; set; } = new();
    public FilterList? Filter { get; set; } = new();
    public GroupByList? GroupBy { get; set; } = new();
    public SortList? Sort { get; set; } = new();
    public Paging? Paging { get; set; } = new();
}
