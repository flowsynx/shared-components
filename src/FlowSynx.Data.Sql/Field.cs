using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class Field
{
    public required string Name { get; set; }
    public string? Alias { get; set; }

    public string GetQuery(Format format, string? tableAlias = "")
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(tableAlias))
            sb.Append(format.FormatTableAlias(tableAlias) + '.');

        sb.Append(format.FormatField(Name));

        if (!string.IsNullOrEmpty(Alias))
        {
            sb.Append(format.AliasOperator);
            sb.Append(format.AliasEscape);
            sb.Append(Alias);
            sb.Append(format.AliasEscape);
        }

        return sb.ToString();
    }
}
