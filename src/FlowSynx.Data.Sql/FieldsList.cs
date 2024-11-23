using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class FieldsList : List<Field>
{
    public string GetQuery(Format format)
    {
        if (Count == 0)
            return "*";

        var sb = new StringBuilder();
        foreach (var field in this)
        {
            if (sb.Length > 0)
                sb.Append(", ");

            sb.Append(field.GetQuery(format));
        }

        return sb.ToString();
    }
}