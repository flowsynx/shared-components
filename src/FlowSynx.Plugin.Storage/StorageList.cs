namespace FlowSynx.Plugin.Storage;

public class StorageList
{
    public string Id { get; set; }
    public string? Kind { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string? Size { get; set; }
    public string? ContentType { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? ModifiedTime { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}