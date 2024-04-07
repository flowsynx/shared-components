namespace FlowSynx.IO.Compression;

public interface ICompression
{
    Task<CompressEntry> Compress(List<CompressEntry> compressEntries);
    Task Decompress(CompressEntry compressEntry, string destinationPath);
}