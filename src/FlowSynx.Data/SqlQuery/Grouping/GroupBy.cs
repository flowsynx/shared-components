using System.Text;

namespace FlowSynx.Data.SqlQuery.Grouping;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class GroupBy
{
    public required string Name { get; set; }

    public string GetQuery(Format format, string? tableAlias = "")
    {
        var sb = new StringBuilder();
        sb.Append(format.FormatField(Name, tableAlias));
        return sb.ToString();
    }
}