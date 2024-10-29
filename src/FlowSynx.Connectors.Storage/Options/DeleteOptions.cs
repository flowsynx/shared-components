namespace FlowSynx.Connectors.Storage.Options;

public class DeleteOptions
{
    public required string Path { get; set; }
    public bool? Purge { get; set; } = false;
}