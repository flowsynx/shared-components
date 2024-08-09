namespace FlowSynx.Plugin.Options;

public class PluginSearchOptions
{
    public string? Include { get; set; }
    public string? Exclude { get; set; }
    public bool CaseSensitive { get; set; } = false;
}