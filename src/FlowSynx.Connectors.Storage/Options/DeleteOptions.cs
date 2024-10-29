namespace FlowSynx.Connectors.Storage.Options;

public class DeleteOptions
{
    public string Path { get; set; } = string.Empty;
    public bool? Purge { get; set; } = false;
}