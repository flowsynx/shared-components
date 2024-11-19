using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class ValueList : List<Value>
{
    public string GetQuery()
    {
        var sb = new StringBuilder();
        foreach (Value value in this)
        {
            if (sb.Length > 0)
                sb.Append(", ");

            sb.Append(value.Expression);
        }
        return sb.ToString();
    }
}