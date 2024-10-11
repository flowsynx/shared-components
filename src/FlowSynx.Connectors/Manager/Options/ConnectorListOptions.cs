namespace FlowSynx.Plugin.Manager.Options;

public class ConnectorListOptions
{
    public string[]? Fields { get; set; }
    public string? Filter { get; set; }
    public bool? CaseSensitive { get; set; } = false;
    public string? Sort { get; set; }
    public string? Limit { get; set; }
}