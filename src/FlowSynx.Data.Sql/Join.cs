using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class Join
{
    public JoinType Type { get; set; }
    public required string Table { get; set; }
    public List<JoinItem> Expressions { get; set; } = new List<JoinItem>();

    public string GetQuery(Format format, string sourceTable)
    {
        var sb = new StringBuilder();
        sb.Append(GetJoinType());
        sb.Append(' ');
        sb.Append(format.FormatTable(Table));
        sb.Append(" ON ");

        var ex = new StringBuilder();
        foreach (var item in Expressions)
        {
            if (ex.Length > 0)
                ex.Append(" AND ");
            else
            {
                ex.Append(format.FormatTable(sourceTable));
                ex.Append('.');
                ex.Append(format.FormatField(item.Name));
                ex.Append('=');
                ex.Append(format.FormatTable(Table));
                ex.Append('.');
                ex.Append(format.FormatField(item.Value));
            }
        }
        sb.Append(ex);
        return sb.ToString();
    }

    private string GetJoinType()
    {
        switch (Type)
        {
            case JoinType.Right:
                return "RIGHT JOIN";
            case JoinType.Left:
                return "LEFT JOIN";
            case JoinType.Full:
                return "FULL JOIN";
            case JoinType.Inner:
            default:
                return "INNER JOIN";
        }
    }
}