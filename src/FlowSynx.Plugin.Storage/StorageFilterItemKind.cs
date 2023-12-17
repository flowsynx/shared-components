using System.Runtime.Serialization;

namespace FlowSynx.Plugin.Storage;

public enum StorageFilterItemKind
{
    File = 0,
    Directory,
    FileAndDirectory
}