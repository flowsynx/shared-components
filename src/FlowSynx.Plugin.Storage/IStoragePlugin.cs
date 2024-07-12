using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Plugin.Storage;

public interface IStoragePlugin : IPlugin, IDisposable
{
    Task<StorageUsage> About(CancellationToken cancellationToken = default);

    Task<IEnumerable<StorageEntity>> ListAsync(string path, StorageSearchOptions searchOptions, 
        StorageListOptions listOptions, StorageHashOptions hashOptions, StorageMetadataOptions metadataOptions, 
        CancellationToken cancellationToken = default);

    Task WriteAsync(string path, StorageStream storageStream, StorageWriteOptions writeOptions, 
        CancellationToken cancellationToken = default);

    Task<StorageRead> ReadAsync(string path, StorageHashOptions hashOptions, CancellationToken cancellationToken = default);

    Task<bool> FileExistAsync(string path, CancellationToken cancellationToken = default);

    Task DeleteAsync(string path, StorageSearchOptions storageSearches, CancellationToken cancellationToken = default);

    Task DeleteFileAsync(string path, CancellationToken cancellationToken = default);

    Task MakeDirectoryAsync(string path, CancellationToken cancellationToken = default);

    Task PurgeDirectoryAsync(string path, CancellationToken cancellationToken = default);

    Task<bool> DirectoryExistAsync(string path, CancellationToken cancellationToken = default);
}