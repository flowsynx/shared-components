namespace FlowSynx.Data.Sql.Builder;

public class CreateOption
{
    public required string Name { get; set; }
    public CreateTableFieldList Fields { get; set; } = new();
}
