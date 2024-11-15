namespace FlowSynx.Data.DataTableQuery.Fields;

public class FieldsList : List<Field>
{
    public string[] GetSql()
    {
        if (Count == 0)
            return Array.Empty<string>();
        
        var result = new List<string>();

        foreach (var field in this)
            result.Add(field.GetSql());

        return result.ToArray();
    }
}