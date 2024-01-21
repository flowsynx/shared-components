namespace FlowSynx.IO.Compression;

public interface IGZipFile
{
    Task Decompression(string sourcePath, string destinationPath, bool overWrite);
}