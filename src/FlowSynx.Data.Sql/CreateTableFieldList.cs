using System.Text;

namespace FlowSynx.Data.Sql;

public class CreateTableFieldList : List<CreateTableField>
{
    public string GetQuery(Format format)
    {
        var sb = new StringBuilder();
        var primaryKeys = new List<string>();
        var sep = false;

        foreach (var field in this)
        {
            if (sep)
                sb.Append(", ");
            else
                sep = true;

            sb.Append(field.GetQuery(format));

            if (field.IsPrimaryKey is true)
                primaryKeys.Add(field.Name);
        }

        if (primaryKeys.Count > 0)
        {
            if (sep)
                sb.Append(", ");

            var keys = string.Join(',', primaryKeys);
            sb.Append($" PRIMARY KEY({keys})");
        }

        return sb.ToString();
    }
}