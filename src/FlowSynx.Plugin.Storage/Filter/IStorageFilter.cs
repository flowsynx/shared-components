using FlowSynx.Plugin.Storage.Abstractions;
using FlowSynx.Plugin.Storage.Abstractions.Options;

namespace FlowSynx.Plugin.Storage.Filter;

public interface IStorageFilter
{
    IEnumerable<StorageEntity> FilterEntitiesList(IEnumerable<StorageEntity> entities,
        StorageSearchOptions storageSearchOptions, StorageListOptions listOptions);
}