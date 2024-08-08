using System.Runtime.Serialization;

namespace FlowSynx.Plugin.Storage.Abstractions;

public enum StorageFilterItemKind
{
    File = 0,
    Directory,
    FileAndDirectory
}