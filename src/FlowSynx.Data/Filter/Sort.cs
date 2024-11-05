namespace FlowSynx.Data.Filter;

public class Sort
{
    public required string Name { get; set; }
    public SortDirection Direction { get; set; }

    public override string ToString()
    {
        return $"{Name} {Direction}";
    }
}
