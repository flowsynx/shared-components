using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class BulkInsertValueList : List<InsertValueList>
{
    public string GetQuery()
    {
        var index = 1;
        var sb = new StringBuilder();
        foreach (var item in this)
        {
            sb.Append('(');
            sb.Append(item.GetQuery());
            sb.Append(')');

            if (index != Count)
                sb.Append(", ");
            index++;
        }
        return sb.ToString();
    }
}