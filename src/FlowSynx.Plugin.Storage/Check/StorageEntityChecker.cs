using FlowSynx.Plugin.Storage.Abstractions;
using FlowSynx.Plugin.Storage.Abstractions.Options;

namespace FlowSynx.Plugin.Storage.Check;

public class StorageEntityChecker : IStorageEntityChecker
{
    public async Task<IEnumerable<StorageCheckResult>> Check(StoragePluginNorms sourceStoragePluginNorms,
        StoragePluginNorms destinationStoragePluginNorms, StorageSearchOptions searchOptions, StorageCheckOptions checkOptions,
        CancellationToken cancellationToken = default)
    {
        var listOptions = new StorageListOptions { Kind = StorageFilterItemKind.File };
        var hashOptions = new StorageHashOptions() { Hashing = checkOptions.CheckHash };
        var metadataOptions = new StorageMetadataOptions() { IncludeMetadata = false };

        var sourceEntities = 
            await sourceStoragePluginNorms.Plugin.ListAsync(sourceStoragePluginNorms.Path,
            searchOptions, listOptions, hashOptions, metadataOptions, cancellationToken);

        var destinationEntities = 
            await destinationStoragePluginNorms.Plugin.ListAsync(destinationStoragePluginNorms.Path,
            searchOptions, listOptions, hashOptions, metadataOptions, cancellationToken);

        var storageSourceEntities = sourceEntities.ToList();
        var storageDestinationEntities = destinationEntities.ToList();

        var existOnSourceEntities = storageSourceEntities
            .Join(storageDestinationEntities, source => source.Id,
                destination => destination.Id,
                (source, destination) => (Source: source, Destination: destination)).ToList();

        var missedOnDestination = storageSourceEntities.Except(existOnSourceEntities.Select(x => x.Source));

        IEnumerable<StorageEntity> missedOnSource = new List<StorageEntity>();
        if (checkOptions.OneWay is false)
        {
            var existOnDestinationEntities = storageDestinationEntities
                .Join(storageSourceEntities, source => source.Id, destination => destination.Id, (source, destination) => source)
                .ToList();

            missedOnSource = storageDestinationEntities.Except(existOnDestinationEntities);
        }

        var result = new List<StorageCheckResult>();
        foreach (var sourceEntity in existOnSourceEntities)
        {
            var state = StorageCheckState.Different;
            if (sourceEntity.Source.Id == sourceEntity.Destination.Id)
            {
                state = checkOptions switch
                {
                    { CheckSize: true, CheckHash: false } => sourceEntity.Source.Size == sourceEntity.Destination.Size
                                                            ? StorageCheckState.Match
                                                            : StorageCheckState.Different,
                    { CheckSize: false, CheckHash: true } => !string.IsNullOrEmpty(sourceEntity.Source.Md5)
                                                           && !string.IsNullOrEmpty(sourceEntity.Destination.Md5)
                                                           && sourceEntity.Source.Md5 == sourceEntity.Destination.Md5
                                                            ? StorageCheckState.Match
                                                            : StorageCheckState.Different,
                    { CheckSize: true, CheckHash: true } => sourceEntity.Source.Size == sourceEntity.Destination.Size
                                                          && !string.IsNullOrEmpty(sourceEntity.Source.Md5)
                                                          && !string.IsNullOrEmpty(sourceEntity.Destination.Md5)
                                                          && sourceEntity.Source.Md5 == sourceEntity.Destination.Md5
                                                            ? StorageCheckState.Match
                                                            : StorageCheckState.Different,
                    _ => state = StorageCheckState.Match,
                };
            }
            result.Add(new StorageCheckResult(sourceEntity.Source, state));
        }

        result.AddRange(missedOnDestination.Select(sourceEntity => new StorageCheckResult (sourceEntity, StorageCheckState.MissedOnDestination)));
        result.AddRange(missedOnSource.Select(sourceEntity => new StorageCheckResult (sourceEntity, StorageCheckState.MissedOnSource)));

        return result;
    }
}