using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Parsers.Exceptions;

public class SizeParserException : FlowSynxException
{
    public SizeParserException(string message) : base(message) { }
    public SizeParserException(string message, Exception inner) : base(message, inner) { }
}