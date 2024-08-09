namespace FlowSynx.Plugin.Storage.Abstractions.Options;

public class StorageListOptions
{
    public StorageFilterItemKind Kind { get; set; } = StorageFilterItemKind.FileAndDirectory;
    public string? Sorting { get; set; }
    public string? MaxResult { get; set; }
}