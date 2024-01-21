using System.IO.Compression;
using System.Text;

namespace FlowSynx.IO.Compression;

public class GZipFile : IGZipFile
{
    public Task Decompression(string sourcePath, string destinationPath, bool overWrite)
    {
        ExtractTarGz(sourcePath, destinationPath);
        return Task.CompletedTask;
    }

    private void ExtractTarGz(string sourcePath, string destinationPath)
    {
        using var fs = File.OpenRead(sourcePath);
        using var stream = new GZipStream(fs, CompressionMode.Decompress);
        var buffer = new byte[1024];

        while (true)
        {
            ReadExactly(stream, buffer, 100);
            var name = Encoding.ASCII.GetString(buffer, 0, 100).Split('\0')[0];
            if (string.IsNullOrWhiteSpace(name))
                break;

            SeekExactly(stream, buffer, 24);

            ReadExactly(stream, buffer, 12);
            var sizeString = Encoding.ASCII.GetString(buffer, 0, 12).Split('\0')[0];
            var size = Convert.ToInt64(sizeString, 8);

            SeekExactly(stream, buffer, 209);

            ReadExactly(stream, buffer, 155);
            var prefix = Encoding.ASCII.GetString(buffer, 0, 155).Split('\0')[0];
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                name = prefix + name;
            }

            SeekExactly(stream, buffer, 12);

            var output = Path.GetFullPath(Path.Combine(destinationPath, name));
            if (!Directory.Exists(Path.GetDirectoryName(output)))
            {
                var path = Path.GetDirectoryName(output);
                if (!string.IsNullOrEmpty(path))
                    Directory.CreateDirectory(path);
            }

            using (var outfs = File.Open(output, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var total = 0;
                while (true)
                {
                    var next = Math.Min(buffer.Length, (int)size - total);
                    ReadExactly(stream, buffer, next);
                    outfs.Write(buffer, 0, next);
                    total += next;
                    if (total == size)
                        break;
                }
            }

            var offset = 512 - ((int)size % 512);
            if (offset == 512)
                offset = 0;

            SeekExactly(stream, buffer, offset);
        }
    }

    private void ReadExactly(Stream stream, byte[] buffer, int count)
    {
        var total = 0;
        while (true)
        {
            var n = stream.Read(buffer, total, count - total);
            total += n;
            if (total == count)
                return;
        }
    }

    private void SeekExactly(Stream stream, byte[] buffer, int count)
    {
        ReadExactly(stream, buffer, count);
    }
}