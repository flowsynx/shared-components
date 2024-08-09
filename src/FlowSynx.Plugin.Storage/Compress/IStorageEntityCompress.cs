using FlowSynx.Plugin.Storage.Abstractions.Options;

namespace FlowSynx.Plugin.Storage.Compress;

public interface IStorageEntityCompress
{
    Task<StorageCompressResult> Compress(StoragePluginNorms storagePluginNorms, StorageSearchOptions searchOptions,
        StorageListOptions listOptions, StorageHashOptions hashOptions, StorageCompressionOptions compressionOptions,
        CancellationToken cancellationToken = default);
}