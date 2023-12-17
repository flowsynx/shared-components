using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.IO.Exceptions;

public class DeserializerException : FlowSynxException
{
    public DeserializerException(string message) : base(message) { }
    public DeserializerException(string message, Exception inner) : base(message, inner) { }
}