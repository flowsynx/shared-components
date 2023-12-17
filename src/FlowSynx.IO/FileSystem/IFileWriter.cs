namespace FlowSynx.IO.FileSystem;

public interface IFileWriter
{
    public bool Write(string path, string contents);
}
