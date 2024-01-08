namespace FlowSynx.Configuration;

public class ConfigurationItem: IEquatable<ConfigurationItem>
{
    public ConfigurationItem(Guid id, string name, string type)
    {
        Id = id;
        Name = name;
        Type = type;
    }

    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public Dictionary<string, string?>? Specifications { get; set; }

    public bool Equals(ConfigurationItem? other)
    {
        if (other == null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Id != other.Id) return false;
        if (Name != other.Name) return false;

        return true;
    }
}