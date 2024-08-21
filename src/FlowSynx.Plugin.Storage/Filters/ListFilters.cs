namespace FlowSynx.Plugin.Storage.Filters;

public class ListFilters
{
    public string? Kind { get; set; }
    public string? Include { get; set; }
    public string? Exclude { get; set; }
    public bool CaseSensitive { get; set; } = false;
    public string? MinAge { get; set; }
    public string? MaxAge { get; set; }
    public string? MinSize { get; set; }
    public string? MaxSize { get; set; }
    public bool? Full { get; set; } = true;
    public bool Recurse { get; set; } = false;
    public string? Sorting { get; set; }
    public string? Limit { get; set; }
    public bool? IncludeMetadata { get; set; } = false;
    public bool? Hashing { get; set; } = false;
}