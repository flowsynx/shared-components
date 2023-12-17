using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.IO.Exceptions;

public class SerializerException : FlowSynxException
{
    public SerializerException(string message) : base(message) { }
    public SerializerException(string message, Exception inner) : base(message, inner) { }
}