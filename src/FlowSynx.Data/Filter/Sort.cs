namespace FlowSynx.Data.Filter;

public class Sort
{
    public required string Name { get; set; }
    public string? Direction { get; set; }

    public override string ToString()
    {
        return $"{Name} {Direction}";
    }
}
