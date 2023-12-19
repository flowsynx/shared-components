namespace FlowSynx.Environment;

public interface IOperatingSystemInfo
{
    public string? Version { get; }
    public string? Type { get; }
    public string? Architecture { get; }

}