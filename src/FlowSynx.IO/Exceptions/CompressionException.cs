using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.IO.Exceptions;

public class CompressionException : FlowSynxException
{
    public CompressionException(string message) : base(message) { }
    public CompressionException(string message, Exception inner) : base(message, inner) { }
}