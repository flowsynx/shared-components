namespace FlowSynx.IO.Compression;

public class ZipFile : IZipFile
{
    public Task Decompression(string sourcePath, string destinationPath, bool overWrite)
    {
        System.IO.Compression.ZipFile.ExtractToDirectory(sourcePath, destinationPath, overWrite);
        return Task.CompletedTask;
    }
}