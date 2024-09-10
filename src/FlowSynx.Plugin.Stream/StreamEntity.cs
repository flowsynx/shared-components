namespace FlowSynx.Plugin.Stream;

public class StreamEntity : IEquatable<StreamEntity>, IComparable<StreamEntity>, ICloneable
{
    public object Row { get; }

    public string? Md5 { get; }

    public StreamEntity(object row)
    {
        Row = row;
        Md5 = Security.HashHelper.Md5.GetHash(row);
    }
    
    public override string ToString()
    {
        const string kind = "row";
        return $"{kind}:{Md5}";
    }

    public override int GetHashCode()
    {
        return string.IsNullOrEmpty(Md5) ? Row.GetHashCode() : Md5.GetHashCode();
    }

    public static bool operator ==(StreamEntity entityA, StreamEntity entityB)
    {
        return entityA.Equals(entityB);
    }

    public static bool operator !=(StreamEntity entityA, StreamEntity entityB)
    {
        return !(entityA == entityB);
    }
    
    public int CompareTo(StreamEntity? other)
    {
        return string.Compare(Md5, other?.Md5, StringComparison.Ordinal);
    }

    public bool Equals(StreamEntity? other)
    {
        if (ReferenceEquals(other, null))
            return false;

        return other.Md5 == Md5;
    }

    public override bool Equals(object? other)
    {
        if (ReferenceEquals(other, null))
            return false;
        if (ReferenceEquals(other, this))
            return true;

        return other is StreamEntity path && Equals(path);
    }

    public object Clone()
    {
        var clone = (StreamEntity)MemberwiseClone();
        return clone;
    }
}