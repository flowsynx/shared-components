namespace FlowSynx.IO.Compression;

public interface IZipFile
{
    Task Decompression(string sourcePath, string destinationPath, bool overWrite);
}