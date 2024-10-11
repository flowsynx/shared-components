namespace FlowSynx.Connectors.Abstractions;

public class ReadResult
{
    public required byte[] Content { get; set; }
    public long Length => Content?.Length ?? 0;
    public string? ContentHash { get; set; }
}