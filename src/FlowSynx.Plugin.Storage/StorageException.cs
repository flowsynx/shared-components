using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Plugin.Storage;

public class StorageException : FlowSynxException
{
    public StorageException(string message) : base(message) { }
    public StorageException(string message, Exception inner) : base(message, inner) { }
}