namespace FlowSynx.Plugin.Storage.Abstractions;

public class StorageSearchOptions
{
    public string? Include { get; set; }
    public string? Exclude { get; set; }
    public string? MinimumAge { get; set; }
    public string? MaximumAge { get; set; }
    public string? MinimumSize { get; set; }
    public string? MaximumSize { get; set; }
    public bool CaseSensitive { get; set; } = false;
    public bool Recurse { get; set; } = false;
}