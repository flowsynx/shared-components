using FlowSynx.Data.Filter;

namespace FlowSynx.Connectors.Manager;

public class ConnectorListOptions
{
    public string[]? Fields { get; set; }
    public string? Filter { get; set; }
    public Sort[]? Sort { get; set; }
    public string? Limit { get; set; }
    public bool? CaseSensitive { get; set; } = false;
}