using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class FieldsList : List<Field>
{
    public string GetQuery(Format format, string? tableAlias = "")
    {
        if (Count == 0)
        {
            return string.IsNullOrEmpty(tableAlias)
                ? "*"
                : format.FormatTableAlias(tableAlias) + ".*";
        }

        var sb = new StringBuilder();
        foreach (var field in this)
        {
            if (sb.Length > 0)
                sb.Append(", ");

            sb.Append(field.GetQuery(format, tableAlias));
        }

        return sb.ToString();
    }
}