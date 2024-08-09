namespace FlowSynx.Plugin.Storage.Abstractions.Models;

public class StorageUsage
{
    public long Total { get; set; }
    public long Free { get; set; }
    public long Used { get; set; }
}