﻿using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace FlowSynx.Security;

public class HashHelper
{
    public class Md5
    {
        public static string GetHash(string input)
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

        public static string GetHash(FileInfo fileInfo)
        {
            try
            {
                using var stream = fileInfo.OpenRead();
                return GetHash(stream);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetHash(Stream stream)
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

        public static string GetHash(object obj)
        {
            try
            {
                using var hasher = MD5.Create();
                using var stream = new MemoryStream();
                var serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
                var hashBytes = hasher.ComputeHash(stream.ToArray());
                return Convert.ToHexString(hashBytes);
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    public class Sha256
    {
        public static string GetHash(string input)
        {
            try
            {
                using var hasher = SHA256.Create();
                var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var hashBytes = hasher.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetHash(FileInfo fileInfo)
        {
            try
            {
                using var stream = fileInfo.OpenRead();
                return GetHash(stream);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetHash(Stream stream)
        {
            try
            {
                using var hasher = SHA256.Create();
                var hashBytes = hasher.ComputeHash(stream);
                return Convert.ToHexString(hashBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetHash(object obj)
        {
            try
            {
                using var hasher = SHA256.Create();
                using var stream = new MemoryStream();
                var serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
                var hashBytes = hasher.ComputeHash(stream.ToArray());
                return Convert.ToHexString(hashBytes);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}