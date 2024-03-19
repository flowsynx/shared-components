namespace FlowSynx.Plugin.Storage;

public class StorageRead
{
    public required StorageStream Stream { get; set; }
    public string? Extension { get; set; }
    public string? MimeType { get; set; }
    public string? Md5 { get; set; }
}