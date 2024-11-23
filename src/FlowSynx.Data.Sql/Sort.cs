using FlowSynx.Data.Sql.Exceptions;
using System.Text;

namespace FlowSynx.Data.Sql;

public class Sort
{
    public required string Name { get; set; }
    public string? Direction { get; set; }

    public string GetQuery(Format format)
    {
        var sb = new StringBuilder();
        sb.Append(format.FormatField(Name) + " " + GetDirection());
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

        throw new DataSqlException(Resources.SortDirectionIsNotSupported);
    }
}
