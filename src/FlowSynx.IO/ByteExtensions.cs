using System;

namespace FlowSynx.IO;

public static class ByteExtensions
{
    public static string ToHexString(this byte[]? bytes)
    {
        return bytes == null ? string.Empty : Convert.ToHexString(bytes);
    }

    public static string ToBase64String(this byte[]? bytes)
    {
        return bytes == null ? string.Empty : Convert.ToBase64String(bytes);
    }

    public static Stream ToStream(this byte[]? bytes)
    {
        return bytes == null ? Stream.Null : new MemoryStream(bytes); ;
    }
}