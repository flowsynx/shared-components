using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Net.Exceptions;

public class MimeTypeException : FlowSynxException
{
    public MimeTypeException(string message) : base(message) { }
    public MimeTypeException(string message, Exception inner) : base(message, inner) { }
}