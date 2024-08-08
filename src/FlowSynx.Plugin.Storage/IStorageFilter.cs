using FlowSynx.Plugin.Storage.Abstractions;

namespace FlowSynx.Plugin.Storage;

public interface IStorageFilter
{
    IEnumerable<StorageEntity> FilterEntitiesList(IEnumerable<StorageEntity> entities, StorageSearchOptions storageSearchOptions, StorageListOptions listOptions);
}