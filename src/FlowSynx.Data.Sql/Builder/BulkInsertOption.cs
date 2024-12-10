namespace FlowSynx.Data.Sql.Builder;

public class BulkInsertOption
{
    public required string Table { get; set; }
    public BulkInsertFieldsList Fields { get; set; } = new();
    public BulkInsertValueList Values { get; set; } = new();
}
