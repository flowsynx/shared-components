namespace FlowSynx.Connectors.Storage.Options;

public class WriteOptions
{
    public string Path { get; set; } = string.Empty;
    public bool? Overwrite { get; set; } = false;
}