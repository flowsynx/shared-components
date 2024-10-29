namespace FlowSynx.Connectors.Storage.Options;

public class CreateOptions
{
    public required string Path { get; set; }
    public bool? Hidden { get; set; } = false;
}