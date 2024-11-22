using System.Text;

namespace FlowSynx.Data.Sql;

public class CreateTableField
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public bool? Nullable { get; set; }
    public bool? IsPrimaryKey { get; set; }
    public string? Default { get; set; }

    public string GetQuery(Format format)
    {
        var sb = new StringBuilder();
        return sb.ToString();
    }
}