﻿using FlowSynx.IO;
using FlowSynx.Plugin.Storage.Abstractions;
using FlowSynx.Plugin.Storage.Abstractions.Exceptions;
using FlowSynx.Plugin.Storage.Abstractions.Options;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Plugin.Storage.Copy;

public class StorageEntityCopier : IStorageEntityCopier
{
    private readonly ILogger<StorageEntityCopier> _logger;

    public StorageEntityCopier(ILogger<StorageEntityCopier> logger)
    {
        _logger = logger;
    }

    public async Task Copy(StoragePluginNorms sourceStoragePluginNorms, StoragePluginNorms destinationStoragePluginNorms,
        StorageSearchOptions searchOptions, StorageCopyOptions copyOptions,
        CancellationToken cancellationToken = default)
    {
        bool isFile;
        if ((isFile = PathHelper.IsFile(sourceStoragePluginNorms.Path)) != PathHelper.IsFile(destinationStoragePluginNorms.Path))
            throw new StorageException(Resources.CopyDestinationPathIsDifferentThanSourcePath);

        if (copyOptions.ClearDestinationPath is true)
        {
            _logger.LogWarning("Purge directory from destination storage before copying.");
            await destinationStoragePluginNorms.Plugin.DeleteAsync(destinationStoragePluginNorms.Path, new StorageSearchOptions(), cancellationToken);
        }

        if (isFile)
        {
            await CopyFile(sourceStoragePluginNorms.Plugin, sourceStoragePluginNorms.Path,
                destinationStoragePluginNorms.Plugin, destinationStoragePluginNorms.Path,
                copyOptions.OverWriteData, cancellationToken);
        }
        else
        {
            await CopyDirectory(sourceStoragePluginNorms.Plugin, sourceStoragePluginNorms.Path,
                destinationStoragePluginNorms.Plugin, destinationStoragePluginNorms.Path,
                searchOptions, copyOptions.OverWriteData, cancellationToken);
        }
    }

    private async Task CopyFile(IStoragePlugin sourcePlugin, string sourceFile, IStoragePlugin destinationPlugin,
    string destinationFile, bool? overWrite, CancellationToken cancellationToken = default)
    {
        var fileExist = await sourcePlugin.FileExistAsync(destinationFile, cancellationToken);
        if (overWrite is null or false && fileExist)
        {
            _logger.LogInformation($"Copy operation ignored - The file '{destinationFile}' is already exist on '{destinationPlugin.Name}'");
            return;
        }

        var sourceStream = await sourcePlugin.ReadAsync(sourceFile, new StorageHashOptions(), cancellationToken);
        await destinationPlugin.WriteAsync(destinationFile, sourceStream.Stream, new StorageWriteOptions() { Overwrite = overWrite }, cancellationToken);
        _logger.LogInformation($"Copy operation - From '{sourcePlugin.Name}' to '{destinationPlugin.Name}' for file '{sourceFile}'");
        sourceStream.Stream.Close();
    }

    private async Task CopyDirectory(IStoragePlugin sourcePlugin, string sourceDirectory,
        IStoragePlugin destinationPlugin, string destinationDirectory,
        StorageSearchOptions searchOptions, bool? overWrite, CancellationToken cancellationToken = default)
    {
        if (!PathHelper.IsRootPath(destinationDirectory))
            await destinationPlugin.MakeDirectoryAsync(destinationDirectory, cancellationToken);

        var listOptions = new StorageListOptions { };
        var hashOptions = new StorageHashOptions() { Hashing = false };
        var metadataOptions = new StorageMetadataOptions() { IncludeMetadata = false };

        var entities = await sourcePlugin.ListAsync(sourceDirectory, searchOptions, 
            listOptions, hashOptions, metadataOptions, cancellationToken);

        var storageEntities = entities.ToList();
        foreach (string dirPath in storageEntities.Where(x => x.Kind == StorageEntityItemKind.Directory))
        {
            var destinationDir = dirPath.Replace(sourceDirectory, destinationDirectory);
            await destinationPlugin.MakeDirectoryAsync(destinationDir, cancellationToken);
            _logger.LogInformation($"Copy operation - From '{sourcePlugin.Name}' to '{destinationPlugin.Name}' for directory '{dirPath}'");
        }

        foreach (string file in storageEntities.Where(x => x.Kind == StorageEntityItemKind.File))
        {
            var destinationFile = file.Replace(sourceDirectory, destinationDirectory);
            await CopyFile(sourcePlugin, file, destinationPlugin, destinationFile, overWrite, cancellationToken);
        }
    }
}