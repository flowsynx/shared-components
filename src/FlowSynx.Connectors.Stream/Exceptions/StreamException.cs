using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Connectors.Stream.Exceptions;

public class StreamException : FlowSynxException
{
    public StreamException(string message) : base(message) { }
    public StreamException(string message, Exception inner) : base(message, inner) { }
}
