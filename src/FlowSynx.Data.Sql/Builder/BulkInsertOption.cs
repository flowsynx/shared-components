namespace FlowSynx.Data.Sql.Builder;

public class BulkInsertOption
{
    public required string Table { get; set; }
    public InsertFieldsList Fields { get; set; } = new();
    public List<InsertValueList> Values { get; set; } = new();
}
