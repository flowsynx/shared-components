using EnsureThat;
using Microsoft.Extensions.Logging;
using FlowSynx.Plugin.Storage.Abstractions;
using FlowSynx.Plugin.Storage.Abstractions.Exceptions;
using FlowSynx.Plugin.Storage.Abstractions.Models;
using FlowSynx.Plugin.Storage.Abstractions.Options;
using FlowSynx.Plugin.Storage.Check;
using FlowSynx.Plugin.Storage.Compress;
using FlowSynx.Plugin.Storage.Copy;
using FlowSynx.Plugin.Storage.Move;

namespace FlowSynx.Plugin.Storage.Services;

public class StorageService : IStorageService
{
    private readonly ILogger<StorageService> _logger;
    private readonly IStorageEntityCopier _storageEntityCopier;
    private readonly IStorageEntityMover _storageEntityMover;
    private readonly IStorageEntityChecker _storageEntityChecker;
    private readonly IStorageEntityCompress _storageEntityCompress;

    public StorageService(ILogger<StorageService> logger,
        IStorageEntityCopier storageEntityCopier, IStorageEntityMover storageEntityMover,
        IStorageEntityChecker storageEntityChecker, IStorageEntityCompress storageEntityCompress)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(storageEntityCopier, nameof(storageEntityCopier));
        EnsureArg.IsNotNull(storageEntityMover, nameof(storageEntityMover));
        EnsureArg.IsNotNull(storageEntityChecker, nameof(storageEntityChecker));
        EnsureArg.IsNotNull(storageEntityCompress, nameof(storageEntityCompress));
        _logger = logger;
        _storageEntityCopier = storageEntityCopier;
        _storageEntityMover = storageEntityMover;
        _storageEntityChecker = storageEntityChecker;
        _storageEntityCompress = storageEntityCompress;
    }

    public async Task<StorageUsage> About(StoragePluginNorms storageNormsInfo, CancellationToken cancellationToken = default)
    {
        try
        {
            return await storageNormsInfo.Plugin.About(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Getting information about a storage. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task<IEnumerable<StorageEntity>> List(StoragePluginNorms storageNormsInfo, StorageSearchOptions searchOptions,
        StorageListOptions listOptions, StorageHashOptions hashOptions, StorageMetadataOptions metadataOptions,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await storageNormsInfo.Plugin.ListAsync(storageNormsInfo.Path, searchOptions,
                listOptions, hashOptions, metadataOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Getting entities list from storage. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task WriteAsync(StoragePluginNorms storageNormsInfo, StorageStream storageStream, StorageWriteOptions writeOptions,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await storageNormsInfo.Plugin.WriteAsync(storageNormsInfo.Path, storageStream, writeOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Read file. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task<StorageRead> ReadAsync(StoragePluginNorms storageNormsInfo, StorageHashOptions hashOptions,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await storageNormsInfo.Plugin.ReadAsync(storageNormsInfo.Path, hashOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Read file. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task Delete(StoragePluginNorms storageNormsInfo, StorageSearchOptions storageSearches, CancellationToken cancellationToken = default)
    {
        try
        {
            await storageNormsInfo.Plugin.DeleteAsync(storageNormsInfo.Path, storageSearches, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Delete files from storage. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task DeleteFile(StoragePluginNorms storageNormsInfo, CancellationToken cancellationToken = default)
    {
        try
        {
            await storageNormsInfo.Plugin.DeleteFileAsync(storageNormsInfo.Path, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Delete file from storage. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task<bool> FileExist(StoragePluginNorms storageNormsInfo, CancellationToken cancellationToken = default)
    {
        try
        {
            return await storageNormsInfo.Plugin.FileExistAsync(storageNormsInfo.Path, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"File exist in storage. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task MakeDirectoryAsync(StoragePluginNorms storageNormsInfo, CancellationToken cancellationToken = default)
    {
        try
        {
            await storageNormsInfo.Plugin.MakeDirectoryAsync(storageNormsInfo.Path, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Make directory from storage. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task PurgeDirectoryAsync(StoragePluginNorms storageNormsInfo, CancellationToken cancellationToken = default)
    {
        try
        {
            await storageNormsInfo.Plugin.PurgeDirectoryAsync(storageNormsInfo.Path, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Purge directory from storage. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task Copy(StoragePluginNorms sourceStorageNormsInfo, StoragePluginNorms destinationStorageNormsInfo,
        StorageSearchOptions searchOptions, StorageCopyOptions copyOptions, CancellationToken cancellationToken = default)
    {
        try
        {
            await _storageEntityCopier.Copy(sourceStorageNormsInfo, destinationStorageNormsInfo, searchOptions,
                copyOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Copy from storage '{sourceStorageNormsInfo.Plugin.Name}' to {destinationStorageNormsInfo.Plugin.Name}. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task Move(StoragePluginNorms sourceStorageNormsInfo, StoragePluginNorms destinationStorageNormsInfo,
        StorageSearchOptions searchOptions, StorageMoveOptions moveOptions, CancellationToken cancellationToken = default)
    {
        try
        {
            await _storageEntityMover.Move(sourceStorageNormsInfo, destinationStorageNormsInfo, searchOptions,
                moveOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Move from storage '{sourceStorageNormsInfo.Plugin.Name}' to {destinationStorageNormsInfo.Plugin.Name}. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task<IEnumerable<StorageCheckResult>> Check(StoragePluginNorms sourceStorageNormsInfo,
        StoragePluginNorms destinationStorageNormsInfo, StorageSearchOptions searchOptions,
        StorageCheckOptions checkOptions, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _storageEntityChecker.Check(sourceStorageNormsInfo, destinationStorageNormsInfo,
                searchOptions, checkOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Move from storage '{sourceStorageNormsInfo.Plugin.Name}' to {destinationStorageNormsInfo.Plugin.Name}. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }

    public async Task<StorageCompressResult> Compress(StoragePluginNorms storageNormsInfo, StorageSearchOptions searchOptions,
        StorageListOptions listOptions, StorageHashOptions hashOptions, StorageCompressionOptions compressionOptions,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _storageEntityCompress.Compress(storageNormsInfo, searchOptions, listOptions, hashOptions,
                compressionOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Getting entities list from storage. Message: {ex.Message}");
            throw new StorageException(ex.Message);
        }
    }
}