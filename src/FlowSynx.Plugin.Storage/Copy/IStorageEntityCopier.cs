using FlowSynx.Plugin.Storage.Abstractions.Options;

namespace FlowSynx.Plugin.Storage.Copy;

public interface IStorageEntityCopier
{
    Task Copy(StoragePluginNorms sourceStoragePluginNorms, StoragePluginNorms destinationStoragePluginNorms,
        StorageSearchOptions searchOptions, StorageCopyOptions copyOptions,
        CancellationToken cancellationToken = default);
}
