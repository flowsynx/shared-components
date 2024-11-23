using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class GroupBy
{
    public required string Name { get; set; }

    public string GetQuery(Format format)
    {
        var sb = new StringBuilder();
        sb.Append(format.FormatField(Name));
        return sb.ToString();
    }
}