namespace FlowSynx.IO.Compression;

public interface ICompression
{
    Task<CompressEntry> Compress(IEnumerable<CompressEntry> compressEntries);
    Task Decompress(CompressEntry compressEntry, string destinationPath);
}