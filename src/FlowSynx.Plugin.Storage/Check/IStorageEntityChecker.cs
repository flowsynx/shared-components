using FlowSynx.Plugin.Storage.Abstractions.Options;

namespace FlowSynx.Plugin.Storage.Check;

public interface IStorageEntityChecker
{
    Task<IEnumerable<StorageCheckResult>> Check(StoragePluginNorms sourceStoragePluginNorms, StoragePluginNorms destinationStoragePluginNorms,
        StorageSearchOptions searchOptions, StorageCheckOptions checkOptions, CancellationToken cancellationToken = default);
}