namespace FlowSynx.IO;

public static class ByteExtensions
{
    public static string ToHexString(this byte[]? bytes)
    {
        return bytes == null ? string.Empty : System.Convert.ToHexString(bytes);
    }
}