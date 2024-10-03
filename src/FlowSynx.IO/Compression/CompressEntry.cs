namespace FlowSynx.IO.Compression;

public class CompressEntry
{
    public required string Name { get; set; }
    public required byte[] Content { get; set; }
    public required string? ContentType { get; set; }
}