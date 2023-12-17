using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.IO.Exceptions;

public class FileWriterException : FlowSynxException
{
    public FileWriterException(string message) : base(message) { }
    public FileWriterException(string message, Exception inner) : base(message, inner) { }
}