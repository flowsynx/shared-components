using System.Text;
using FlowSynx.Data.Extensions;

namespace FlowSynx.Data.Sql.Grouping;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class GroupBy
{
    public required string Name { get; set; }

    public string GetSql(ISqlFormat format, string? tableAlias = "")
    {
        var sb = new StringBuilder();
        sb.Append(format.FormatField(Name, tableAlias));
        return sb.ToString();
    }
}