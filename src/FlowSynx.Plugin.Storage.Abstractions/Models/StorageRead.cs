namespace FlowSynx.Plugin.Storage.Abstractions.Models;

public class StorageRead
{
    public required StorageStream Stream { get; set; }
    public string? Extension { get; set; }
    public string? ContentType { get; set; }
    public string? Md5 { get; set; }
}