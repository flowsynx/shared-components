using EnsureThat;
using FlowSynx.IO;
using FlowSynx.Net;
using FlowSynx.Plugin.Storage.Abstractions.Exceptions;
using FlowSynx.Security;

namespace FlowSynx.Plugin.Storage;

public class StorageEntity : IEquatable<StorageEntity>, IComparable<StorageEntity>, ICloneable
{
    public string Id => HashHelper.Md5.GetHash(this.ToString());

    public string Kind { get; }

    private bool IsFile => Kind == StorageEntityItemKind.File;

    public string DirectoryPath { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public long? Size { get; set; }

    public string? ContentType => IsFile ? GetExtension().GetContentType() : "";

    public string? Md5 { get; set; }

    public DateTimeOffset? CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public string FullPath =>
        Kind == StorageEntityItemKind.Directory
            ? PathHelper.IsRootPath(Name)
                ? PathHelper.Combine(DirectoryPath, Name)
                : PathHelper.AddTrailingPathSeparator(PathHelper.Combine(DirectoryPath, Name))
            : PathHelper.Combine(DirectoryPath, Name);

    protected bool IsRootFolder => Kind == StorageEntityItemKind.Directory && PathHelper.IsRootPath(FullPath);

    public Dictionary<string, object> Metadata { get; private set; } = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

    public bool TryGetMetadata<TValue>(string name, out TValue value, TValue defaultValue)
    {
        if (string.IsNullOrEmpty(name) || !Metadata.TryGetValue(name, out var objValue))
        {
            value = defaultValue;
            return false;
        }

        if (objValue is TValue val)
        {
            value = (TValue)val;
            return true;
        }

        value = defaultValue;
        return false;
    }

    public void TryAddMetadata(params object[] keyValues)
    {
        for (var i = 0; i < keyValues.Length; i += 2)
        {
            var key = (string)keyValues[i];
            var value = keyValues[i + 1];

            if (key != null && value != null)
            {
                if (value is string s && string.IsNullOrEmpty(s))
                    continue;

                Metadata[key] = value;
            }
        }
    }

    public StorageEntity(string fullPath, string kind)
    {
        SetFullPath(fullPath);
        Kind = kind;
    }

    public StorageEntity(string folderPath, string name, string kind)
    {
        EnsureArg.IsNotNullOrEmpty(name, nameof(name));
        Name = name;
        Name = PathHelper.NormalizePart(Name);
        DirectoryPath = PathHelper.Normalize(folderPath);
        Kind = kind;
    }

    public string? GetExtension()
    {
        if (!IsFile)
            throw new StorageException(Resources.StorageEntityGetExtensionTheSpecifiedPathIsNotAFile);

        var name = Name;
        var extensionIndex = name?.LastIndexOf('.') ?? -1;
        return extensionIndex < 0 ? "" : name?[extensionIndex..];
    }

    public override int GetHashCode()
    {
        return FullPath.GetHashCode() * Kind.GetHashCode();
    }

    public override string ToString()
    {
        var kind = Kind == StorageEntityItemKind.File ? "file" : "directory";
        return $"{kind}:{Name}";
    }

    public static implicit operator StorageEntity(string fullPath)
    {
        return new StorageEntity(fullPath, StorageEntityItemKind.File);
    }

    public static implicit operator string(StorageEntity storageEntity)
    {
        return storageEntity.FullPath;
    }

    public static bool operator ==(StorageEntity pathA, StorageEntity pathB)
    {
        return pathA.Equals(pathB);
    }

    public static bool operator !=(StorageEntity pathA, StorageEntity pathB)
    {
        return !(pathA == pathB);
    }

    private void SetFullPath(string fullPath)
    {
        var path = PathHelper.Normalize(fullPath);

        if (PathHelper.IsRootPath(path))
        {
            Name = PathHelper.RootDirectoryPath;
            DirectoryPath = PathHelper.RootDirectoryPath;
        }
        else
        {
            var parts = PathHelper.Split(path);

            Name = parts.Last();
            DirectoryPath = PathHelper.GetParent(path);
        }
    }
    public int CompareTo(StorageEntity? other)
    {
        return string.Compare(FullPath, other?.FullPath, StringComparison.Ordinal);
    }

    public bool Equals(StorageEntity? other)
    {
        if (ReferenceEquals(other, null))
            return false;

        return other.FullPath == FullPath && other.Kind == Kind;
    }

    public override bool Equals(object? other)
    {
        if (ReferenceEquals(other, null))
            return false;
        if (ReferenceEquals(other, this))
            return true;

        return other is StorageEntity path && Equals(path);
    }

    public object Clone()
    {
        var clone = (StorageEntity)MemberwiseClone();
        clone.Metadata = new Dictionary<string, object>(Metadata, StringComparer.OrdinalIgnoreCase);
        return clone;
    }
}