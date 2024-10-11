namespace FlowSynx.IO.Compression;

public class CompressResult
{
    public required byte[] Content { get; set; }
    public long Length => Content?.Length ?? 0;
    public string? ContentType { get; set; }
    public string? ContentHash { get; set; }
}