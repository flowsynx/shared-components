using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Writers;

namespace FlowSynx.IO.Compression;

public class GZipCompression : ICompression
{
    public Task<CompressEntry> Compress(List<CompressEntry> entries)
    {
        var outputMemStream = new MemoryStream();
        var tarOutputMemStream = new MemoryStream();

        using (var writer = WriterFactory.Open(tarOutputMemStream, ArchiveType.Tar, SharpCompress.Common.CompressionType.None))
        {
            foreach (var entry in entries)
            {
                writer.Write(entry.Name, entry.Stream);
            }
        }

        tarOutputMemStream.Position = 0;
        using (var writer = WriterFactory.Open(outputMemStream, ArchiveType.GZip, SharpCompress.Common.CompressionType.GZip))
        {
            writer.Write(Guid.NewGuid().ToString(), tarOutputMemStream);
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
        var tarEntities = new List<string>();
        using (var archive = ArchiveFactory.Open(compressEntry.Stream))
        {
            foreach (var entry in archive.Entries.Where(e => !e.IsDirectory))
            {
                entry.WriteToDirectory(destinationPath, new ExtractionOptions
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
                tarEntities.Add(Path.Combine(destinationPath, entry.Key));
            }
        }

        DecompressTar(tarEntities, destinationPath);

        return Task.CompletedTask;
    }

    private void DecompressTar(IEnumerable<string> tarEntities, string destinationPath)
    {
        foreach (var tarEntity in tarEntities)
        {
            using (var archive = ArchiveFactory.Open(tarEntity))
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
            File.Delete(tarEntity);
        }
    }
}