using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Parsers.Exceptions;

public class PercentParserException : FlowSynxException
{
    public PercentParserException(string message) : base(message) { }
    public PercentParserException(string message, Exception inner) : base(message, inner) { }
}