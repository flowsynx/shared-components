using FlowSynx.IO;
using FlowSynx.Plugin.Storage.Abstractions;
using FlowSynx.Plugin.Storage.Abstractions.Exceptions;
using FlowSynx.Plugin.Storage.Abstractions.Options;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Plugin.Storage.Move;

public class StorageEntityMover : IStorageEntityMover
{
    private readonly ILogger<StorageEntityMover> _logger;

    public StorageEntityMover(ILogger<StorageEntityMover> logger)
    {
        _logger = logger;
    }

    public async Task Move(StoragePluginNorms sourceStoragePluginNorms, StoragePluginNorms destinationStoragePluginNorms, StorageSearchOptions searchOptions, StorageMoveOptions moveOptions, CancellationToken cancellationToken = default)
    {
        if (string.Equals(sourceStoragePluginNorms.Plugin.Name, destinationStoragePluginNorms.Plugin.Name, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(sourceStoragePluginNorms.Path, destinationStoragePluginNorms.Path, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new StorageException(Resources.MoveTheSourceAndDestinationPathAreIdenticalAndOverlap);
        }

        bool isFile;
        if ((isFile = PathHelper.IsFile(sourceStoragePluginNorms.Path)) != PathHelper.IsFile(destinationStoragePluginNorms.Path))
            throw new StorageException(Resources.MoveDestinationPathIsDifferentThanSourcePath);

        if (isFile)
            await MoveFile(sourceStoragePluginNorms.Plugin, sourceStoragePluginNorms.Path, destinationStoragePluginNorms.Plugin, destinationStoragePluginNorms.Path, cancellationToken);
        else
            await MoveDirectory(sourceStoragePluginNorms.Plugin, sourceStoragePluginNorms.Path, destinationStoragePluginNorms.Plugin, destinationStoragePluginNorms.Path, searchOptions, cancellationToken);

        if (!PathHelper.IsRootPath(sourceStoragePluginNorms.Path))
            await sourceStoragePluginNorms.Plugin.DeleteAsync(sourceStoragePluginNorms.Path, searchOptions, cancellationToken);
    }

    private async Task MoveFile(IStoragePlugin sourcePlugin, string sourceFile, IStoragePlugin destinationPlugin, string destinationFile, CancellationToken cancellationToken = default)
    {
        var sourceStream = await sourcePlugin.ReadAsync(sourceFile, new StorageHashOptions(), cancellationToken);
        await destinationPlugin.WriteAsync(destinationFile, sourceStream.Stream, new StorageWriteOptions() { Overwrite = true }, cancellationToken);
        _logger.LogInformation($"Move operation - From '{sourcePlugin.Name}' to '{destinationPlugin.Name}' for file '{sourceFile}'");
        sourceStream.Stream.Close();
    }

    private async Task MoveDirectory(IStoragePlugin sourcePlugin, string sourceDirectory, IStoragePlugin destinationPlugin, string destinationDirectory, StorageSearchOptions searchOptions, CancellationToken cancellationToken = default)
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
            _logger.LogInformation($"Move operation - From '{sourcePlugin.Name}' to '{destinationPlugin.Name}' for directory '{dirPath}'");
        }

        var files = storageEntities.Where(x => x.Kind == StorageEntityItemKind.File).ToList();
        if (!files.Any())
        {
            throw new StorageException($"No files found in the path '{sourceDirectory}'");
        }

        foreach (string file in files)
        {
            var destinationFile = file.Replace(sourceDirectory, destinationDirectory);
            await MoveFile(sourcePlugin, file, destinationPlugin, destinationFile, cancellationToken);
        }
    }
}