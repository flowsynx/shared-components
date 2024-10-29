namespace FlowSynx.Connectors.Storage.Options;

public class WriteOptions
{
    public required string Path { get; set; }
    public bool? Overwrite { get; set; } = false;
}