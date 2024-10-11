namespace FlowSynx.Connectors.Manager;

internal class ConnectorResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Type { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}