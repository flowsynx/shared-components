using EnsureThat;
using FlowSynx.IO.Compression;
using FlowSynx.Plugin.Storage.Abstractions.Exceptions;
using FlowSynx.Plugin.Storage.Abstractions.Options;
using FlowSynx.Security;

namespace FlowSynx.Plugin.Storage.Compress;

public class StorageEntityCompress : IStorageEntityCompress
{
    private readonly Func<CompressType, ICompression> _compressionFactory;

    public StorageEntityCompress(Func<CompressType, ICompression> compressionFactory)
    {
        EnsureArg.IsNotNull(compressionFactory, nameof(compressionFactory));
        _compressionFactory = compressionFactory;
    }

    public async Task<StorageCompressResult> Compress(StoragePluginNorms storagePluginNorms, StorageSearchOptions searchOptions,
        StorageListOptions listOptions, StorageHashOptions hashOptions, StorageCompressionOptions compressionOptions,
        CancellationToken cancellationToken = default)
    {
        var metadataOptions = new StorageMetadataOptions() { IncludeMetadata = false };

        var entities = await storagePluginNorms.Plugin.ListAsync(storagePluginNorms.Path, searchOptions,
                listOptions, new StorageHashOptions(), metadataOptions, cancellationToken);

        var storageEntities = entities.Where(x => x.IsFile).ToList();
        if (storageEntities == null || !storageEntities.Any())
        {
            throw new StorageException("No file found to make a compression archive.");
        }

        var en = new List<CompressEntry>();
        foreach (var entity in storageEntities)
        {
            var stream = await storagePluginNorms.Plugin.ReadAsync(entity.FullPath, new StorageHashOptions(), cancellationToken);
            en.Add(new CompressEntry
            {
                Name = entity.Name,
                ContentType = entity.ContentType,
                Stream = stream.Stream
            });
        }

        var compressResult = await _compressionFactory(compressionOptions.CompressType).Compress(en);
        var md5Hash = string.Empty;

        if (hashOptions.Hashing is true)
        {
            md5Hash = HashHelper.Md5.GetHash(compressResult.Stream);
        }

        return new StorageCompressResult { Stream = compressResult.Stream, ContentType = compressResult.ContentType, Md5 = md5Hash };
    }
}