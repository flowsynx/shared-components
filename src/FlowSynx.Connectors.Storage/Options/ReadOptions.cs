namespace FlowSynx.Connectors.Storage.Options;

public class ReadOptions
{
    public required string Path { get; set; }
    public bool? Hashing { get; set; } = false;
}