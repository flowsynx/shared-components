using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Writers;

namespace FlowSynx.IO.Compression;

public class TarCompression : ICompression
{
    public Task<CompressEntry> Compress(IEnumerable<CompressEntry> entries)
    {
        var outputMemStream = new MemoryStream();
        using (var writer = WriterFactory.Open(outputMemStream, ArchiveType.Tar, SharpCompress.Common.CompressionType.None))
        {
            foreach (var entry in entries)
            {
                writer.Write(entry.Name, entry.Content.ToStream());
            }
        }

        outputMemStream.Position = 0;

        return Task.FromResult(new CompressEntry
        {
            Name = Guid.NewGuid().ToString(),
            ContentType = "application/octet-stream",
            Content = outputMemStream.ToArray() 
        });
    }

    public Task Decompress(CompressEntry compressEntry, string destinationPath)
    {
        using (var archive = ArchiveFactory.Open(compressEntry.Content.ToStream()))
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