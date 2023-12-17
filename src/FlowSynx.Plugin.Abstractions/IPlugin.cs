namespace FlowSynx.Plugin.Abstractions;

public interface IPlugin
{
    Guid Id { get; }
    string Name { get; }
    PluginNamespace Namespace { get; }
    string Type => $"FlowSynx.{Namespace}/{Name}";
    string? Description { get; }
}