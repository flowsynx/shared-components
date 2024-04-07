using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Writers;

namespace FlowSynx.IO.Compression;

public class ZipCompression : ICompression
{
    public Task<CompressEntry> Compress(List<CompressEntry> compressEntries)
    {
        var outputMemStream = new MemoryStream();
        using (var writer = WriterFactory.Open(outputMemStream, ArchiveType.Zip, SharpCompress.Common.CompressionType.Deflate))
        {
            foreach (var entry in compressEntries)
            {
                writer.Write(entry.Name, entry.Stream);
            }
        }

        outputMemStream.Position = 0;

        return Task.FromResult(new CompressEntry
        {
            Name = Guid.NewGuid().ToString(),
            ContentType = "application/octet-stream",
            Stream = outputMemStream
        });
    }

    public Task Decompress(CompressEntry compressEntry, string destinationPath)
    {
        using (var archive = ArchiveFactory.Open(compressEntry.Stream))
        {
            foreach (var entry in archive.Entries.Where(e => !e.IsDirectory))
            {
                entry.WriteToDirectory(destinationPath, new ExtractionOptions
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }
        }
        return Task.CompletedTask;
    }
}