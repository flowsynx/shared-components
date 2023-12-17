using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.IO.Exceptions;

public class FileReaderException : FlowSynxException
{
    public FileReaderException(string message) : base(message) { }
    public FileReaderException(string message, Exception inner) : base(message, inner) { }
}