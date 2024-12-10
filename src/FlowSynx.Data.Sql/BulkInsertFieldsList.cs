using System.Text;

namespace FlowSynx.Data.Sql;

public class BulkInsertFieldsList : List<string>
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

            sb.Append(field);
        }

        return sb.ToString();
    }
}