using System.Text;
using FlowSynx.Data.Extensions;

namespace FlowSynx.Data.Sql.Sorting;

public class Sort
{
    public required string Name { get; set; }
    public string? Direction { get; set; }

    public string GetSql(Format format, string? tableAlias = "")
    {
        var sb = new StringBuilder();
        sb.Append(format.FormatField(Name, tableAlias) + " " + GetDirection());
        return sb.ToString();
    }

    private string GetDirection()
    {
        if (string.IsNullOrEmpty(Direction))
            return "ASC";

        if (Direction.Equals("ASC", StringComparison.OrdinalIgnoreCase))
            return "ASC";

        if (Direction.Equals("DESC", StringComparison.OrdinalIgnoreCase))
            return "DESC";

        throw new Exception("Sort direction is not supported. It should be ASC or DESC");
    }
}
