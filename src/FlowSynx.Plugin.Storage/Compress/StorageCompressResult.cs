namespace FlowSynx.Plugin.Storage.Compress;

public class StorageCompressResult
{
    public required Stream Stream { get; set; }
    public string? ContentType { get; set; }
    public string? Md5 { get; set; }
}