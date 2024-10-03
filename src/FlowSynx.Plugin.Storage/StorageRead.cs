namespace FlowSynx.Plugin.Storage;

public class StorageRead
{
    public required byte[] Content { get; set; }
    public string? ContentType { get; set; }
    public string? Extension { get; set; }
    public string? Md5 { get; set; }
}