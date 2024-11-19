using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class JoinList : List<Join>
{
    public string GetQuery(Format format, string sourceTable)
    {
        var sb = new StringBuilder();
        foreach (var join in this)
        {
            if (sb.Length > 0)
                sb.Append(' ');
            sb.Append(join.GetQuery(format, sourceTable));
        }
        return sb.ToString();
    }
}