namespace FlowSynx.Connectors.Storage.Options;

public class ReadOptions
{
    public string Path { get; set; } = string.Empty;
    public bool? Hashing { get; set; } = false;
}