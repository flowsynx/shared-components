using FlowSynx.Data.DataTableQuery.Extensions.Exceptions;
using System.Text;

namespace FlowSynx.Data.DataTableQuery.Sorting;

public class Sort
{
    public required string Name { get; set; }
    public string? Direction { get; set; }

    public string GetQuery()
    {
        var sb = new StringBuilder();
        sb.Append(Name + " " + GetDirection());
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

        throw new DataTableQueryException(Resources.SortDirectionIsNotSupported);
    }
}
