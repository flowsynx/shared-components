namespace FlowSynx.Plugin.Abstractions;

public interface IPlugin
{
    Task Initialize();

    Guid Id { get; }

    string Name { get; }

    PluginNamespace Namespace { get; }

    string Type => $"FlowSynx.{Namespace}/{Name}";

    string? Description { get; }

    Dictionary<string, string?>? Specifications { get; set; }

    Type SpecificationsType { get; }
}