using System.Security.Cryptography;

namespace FlowSynx.Security;

public class HashHelper
{
    public static string GetMd5Hash(string input)
    {
        try
        {
            using var hasher = MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            var hashBytes = hasher.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string GetMd5HashFile(string path)
    {
        try
        {
            using var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return GetMd5HashFile(stream);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string GetMd5HashFile(Stream stream)
    {
        try
        {
            using var hasher = MD5.Create();
            var hashBytes = hasher.ComputeHash(stream);
            return Convert.ToHexString(hashBytes);
        }
        catch
        {
            return string.Empty;
        }
    }
}