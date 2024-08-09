using FlowSynx.Plugin.Storage;
using FlowSynx.Plugin.Storage.Abstractions;

namespace FlowSynx.Plugin.Storage.Check;

public class StorageCheckResult
{
    public StorageCheckResult(StorageEntity entity, StorageCheckState state)
    {
        Entity = entity;
        State = state;
    }

    public StorageEntity Entity { get; set; }
    public StorageCheckState State { get; set; }
}