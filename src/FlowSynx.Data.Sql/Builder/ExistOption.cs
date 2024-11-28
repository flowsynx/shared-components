namespace FlowSynx.Data.Sql.Builder;

public class ExistOption
{
    public required string Table { get; set; }
    public FilterList? Filter { get; set; } = new();
}
