using FlowSynx.Plugin.Storage.Abstractions.Options;

namespace FlowSynx.Plugin.Storage.Move;

public interface IStorageEntityMover
{
    Task Move(StoragePluginNorms sourceStoragePluginNorms, StoragePluginNorms destinationStoragePluginNorms,
        StorageSearchOptions searchOptions, StorageMoveOptions moveOptions,
        CancellationToken cancellationToken = default);
}
