using FlowSynx.Plugin.Storage.Abstractions;
using FlowSynx.Plugin.Storage.Abstractions.Models;
using FlowSynx.Plugin.Storage.Abstractions.Options;
using FlowSynx.Plugin.Storage.Check;
using FlowSynx.Plugin.Storage.Compress;
using FlowSynx.Plugin.Storage.Copy;
using FlowSynx.Plugin.Storage.Move;

namespace FlowSynx.Plugin.Storage.Services;

internal interface IStorageService
{
    Task<StorageUsage> About(StoragePluginNorms storagePluginNorms, CancellationToken cancellationToken = default);

    Task<IEnumerable<StorageEntity>> List(StoragePluginNorms storagePluginNorms, StorageSearchOptions searchOptions,
        StorageListOptions listOptions, StorageHashOptions hashOptions, StorageMetadataOptions metadataOptions,
        CancellationToken cancellationToken = default);

    Task WriteAsync(StoragePluginNorms storagePluginNorms, StorageStream storageStream, StorageWriteOptions writeOptions,
        CancellationToken cancellationToken = default);

    Task<StorageRead> ReadAsync(StoragePluginNorms storagePluginNorms, StorageHashOptions hashOptions,
        CancellationToken cancellationToken = default);

    Task Delete(StoragePluginNorms storagePluginNorms, StorageSearchOptions storageSearches, CancellationToken cancellationToken = default);

    Task DeleteFile(StoragePluginNorms storagePluginNorms, CancellationToken cancellationToken = default);

    Task<bool> FileExist(StoragePluginNorms storagePluginNorms, CancellationToken cancellationToken = default);

    Task MakeDirectoryAsync(StoragePluginNorms storagePluginNorms, CancellationToken cancellationToken = default);

    Task PurgeDirectoryAsync(StoragePluginNorms storagePluginNorms, CancellationToken cancellationToken = default);

    Task Copy(StoragePluginNorms sourceStoragePluginNorms, StoragePluginNorms destinationStoragePluginNorms,
        StorageSearchOptions searchOptions, StorageCopyOptions copyOptions, CancellationToken cancellationToken = default);

    Task Move(StoragePluginNorms sourceStoragePluginNorms, StoragePluginNorms destinationStoragePluginNorms,
        StorageSearchOptions searchOptions, StorageMoveOptions moveOptions, CancellationToken cancellationToken = default);

    Task<IEnumerable<StorageCheckResult>> Check(StoragePluginNorms sourceStoragePluginNorms, StoragePluginNorms destinationStoragePluginNorms,
        StorageSearchOptions searchOptions, StorageCheckOptions checkOptions, CancellationToken cancellationToken = default);

    Task<StorageCompressResult> Compress(StoragePluginNorms storagePluginNorms, StorageSearchOptions searchOptions,
        StorageListOptions listOptions, StorageHashOptions hashOptions, StorageCompressionOptions compressionOptions,
        CancellationToken cancellationToken = default);
}