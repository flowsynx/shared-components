using FlowSynx.IO.Compression;

namespace FlowSynx.Plugin.Storage.Compress;

public class StorageCompressionOptions
{
    public CompressType CompressType { get; set; } = CompressType.Zip;
}