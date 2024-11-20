namespace FlowSynx.Data.Sql.Builder;

public class InsertOption
{
    public required Table Table { get; set; }
    public FieldsList Fields { get; set; } = new();
    public ValueList Values { get; set; } = new();
}
