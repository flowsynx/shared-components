namespace FlowSynx.Configuration;

public class ConfigurationResult
{
    public Guid Id { get; }

    public ConfigurationResult(Guid id)
    {
        Id = id;
    }
}