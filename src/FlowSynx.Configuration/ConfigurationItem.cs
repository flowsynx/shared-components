namespace FlowSynx.Configuration;

public class ConfigurationItem
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
}