using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Plugin.Storage;

public interface IStorageFilter
{
    IEnumerable<StorageEntity> Filter(IEnumerable<StorageEntity> entities, PluginOptions? options);
}