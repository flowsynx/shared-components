namespace FlowSynx.Plugin.Storage;

public class StorageListOptions
{
    public StorageFilterItemKind Kind { get; set; } = StorageFilterItemKind.FileAndDirectory;
    public string? Sorting { get; set; }
    public string? MaxResult { get; set; }
}