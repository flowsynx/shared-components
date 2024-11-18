namespace FlowSynx.Data;

public class FieldsList : List<Field>
{
    public void Append(string name)
    {
        Append(new Field { Name = name });
    }

    public void Append(Field field)
    {
        Add(field);
    }

    public string[] GetQuery()
    {
        return Count == 0
            ? Array.Empty<string>()
            : this.Select(field => field.GetQuery()).ToArray();
    }
}