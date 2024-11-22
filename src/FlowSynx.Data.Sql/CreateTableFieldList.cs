using System.Text;

namespace FlowSynx.Data.Sql;

public class CreateTableFieldList : List<CreateTableField>
{
    public string GetQuery(Format format, string? tableAlias = "")
    {
        var sb = new StringBuilder();
        return sb.ToString();
    }
}