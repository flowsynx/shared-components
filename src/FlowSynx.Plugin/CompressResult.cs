namespace FlowSynx.Plugin;

public class CompressResult
{
    public required Stream Content { get; set; }
    public long Length => Content?.Length ?? 0;
    public string? ContentType { get; set; }
    public string? Md5 { get; set; }
}