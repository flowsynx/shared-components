namespace FlowSynx.IO;

public static class ByteExtensions
{
    public static string ToHexString(this byte[]? bytes)
    {
        return bytes == null ? string.Empty : Convert.ToHexString(bytes);
    }

    public static string ToBase64(this byte[]? bytes)
    {
        return bytes == null ? string.Empty : Convert.ToBase64String(bytes);
    }
}