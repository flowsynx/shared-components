namespace FlowSynx.Configuration;

public class ConfigurationSearchOptions
{
    public string? Include { get; set; }
    public string? Exclude { get; set; }
    public string? MinimumAge { get; set; }
    public string? MaximumAge { get; set; }
    public bool CaseSensitive { get; set; } = false;
}