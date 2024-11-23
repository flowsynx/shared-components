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
        sb.Append(format.FormatField(Name) + " " + Type);

        var nullable = Nullable is true ? "NULL" : "NOT NULL";
        sb.Append($" {nullable}");

        if (!string.IsNullOrEmpty(Default))
            sb.Append($" DEFAULT {Default}");

        return sb.ToString();
    }
}