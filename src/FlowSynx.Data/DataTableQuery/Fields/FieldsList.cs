namespace FlowSynx.Data.DataTableQuery.Fields;

public class FieldsList : List<Field>
{
    public string[] GetQuery()
    {
        if (Count == 0)
            return Array.Empty<string>();
        
        var result = new List<string>();

        foreach (var field in this)
            result.Add(field.GetQuery());

        return result.ToArray();
    }
}