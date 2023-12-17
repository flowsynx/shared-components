using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Parsers.Exceptions;

public class DateParserException : FlowSynxException
{
    public DateParserException(string message) : base(message) { }
    public DateParserException(string message, Exception inner) : base(message, inner) { }
}