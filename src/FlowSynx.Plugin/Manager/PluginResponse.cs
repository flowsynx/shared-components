namespace FlowSynx.Plugin.Manager;

internal class PluginResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Type { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}