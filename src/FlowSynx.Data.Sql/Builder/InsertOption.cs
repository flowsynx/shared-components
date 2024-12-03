namespace FlowSynx.Data.Sql.Builder;

public class InsertOption
{
    public required string Table { get; set; }
    public InsertFieldsList Fields { get; set; } = new();
    public InsertValueList Values { get; set; } = new();
}
