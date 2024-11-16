namespace FlowSynx.Configuration.Options;

public class ConfigurationListOptions
{
    public string? Fields { get; set; }
    public string? Filters { get; set; }
    public string? Sorts { get; set; }
    public string? Paging { get; set; }
    public bool? CaseSensitive { get; set; } = false;
}