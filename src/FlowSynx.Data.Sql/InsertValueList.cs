using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class InsertValueList : List<object>
{
    public string GetQuery()
    {
        var sb = new StringBuilder();
        foreach (var value in this)
        {
            if (sb.Length > 0)
                sb.Append(", ");

            if (value is string)
                sb.Append($"'{value}'");
            else
                sb.Append(value);
        }
        return sb.ToString();
    }
}