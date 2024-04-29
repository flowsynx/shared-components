namespace FlowSynx.IO;

public static class StreamExtensions
{
    public static string ConvertToBase64(this Stream stream)
    {
        byte[] bytes;
        using (var memoryStream = new MemoryStream())
        {
            stream.CopyTo(memoryStream);
            bytes = memoryStream.ToArray();
        }

        return Convert.ToBase64String(bytes);
    }

    public static void WriteTo(this Stream stream, string path)
    {
        using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            stream.CopyTo(fileStream);
        }
    }
}