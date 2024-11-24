namespace FlowSynx.Data.Sql.Builder;

public class CreateOption
{
    public required string Table { get; set; }
    public CreateTableFieldList Fields { get; set; } = new();
}
