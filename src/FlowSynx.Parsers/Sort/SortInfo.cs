namespace FlowSynx.Parsers.Sort;

public class SortInfo: IEquatable<SortInfo>
{
    public required string Name { get; init; }
    public required SortDirection Direction { get; init; }

    public bool Equals(SortInfo? other)
    {
        if (other == null)
            return false;
        
        if (ReferenceEquals(this, other))
            return true;
        
        if (Name != other.Name) return false;
        if (Direction != other.Direction) return false;
        
        return true;
    }
}