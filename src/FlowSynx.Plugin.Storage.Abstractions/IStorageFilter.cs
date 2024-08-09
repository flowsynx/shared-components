using FlowSynx.Plugin.Storage.Abstractions.Options;

namespace FlowSynx.Plugin.Storage.Abstractions;

public interface IStorageFilter
{
    IEnumerable<StorageEntity> FilterEntitiesList(IEnumerable<StorageEntity> entities,
        StorageSearchOptions storageSearchOptions, StorageListOptions listOptions);
}