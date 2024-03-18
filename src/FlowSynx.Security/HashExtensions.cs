using System.Security.Cryptography;

namespace FlowSynx.Security;

public class HashHelper
{
    public static string GetMd5Hash(string input)
    {
        using var hasher = MD5.Create();
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        var hashBytes = hasher.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }

    public static string GetMd5HashFile(string path)
    {
        using var stream = System.IO.File.OpenRead(path);
        return GetMd5HashFile(stream);
    }

    public static string GetMd5HashFile(Stream stream)
    {
        using var hasher = MD5.Create();
        var hashBytes = hasher.ComputeHash(stream);
        return Convert.ToHexString(hashBytes);
    }
}