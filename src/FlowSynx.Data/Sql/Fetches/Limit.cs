using System.Text;

namespace FlowSynx.Data.Sql.Fetches;

public class Fetch
{
    public int? Limit { get; set; } = 0;
    public int? OffSet { get; set; } = 0;

    public string GetSql(Format format)
    {
        /*
         OFFSET 0 ROWS 
        FETCH FIRST 10 ROWS ONLY;
        */

        var sb = new StringBuilder();
        if (Limit > 0)
        {
            sb.Append($"LIMIT {Limit}");
        }

        if (OffSet > 0 && sb.Length > 0)
        {
            sb.Append(" ");
            sb.Append($"OFFSET {OffSet}");
        }

        return sb.ToString();
    }
}