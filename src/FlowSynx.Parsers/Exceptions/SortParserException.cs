using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Parsers.Exceptions;

public class SortParserException : FlowSynxException
{
    public SortParserException(string message) : base(message) { }
    public SortParserException(string message, Exception inner) : base(message, inner) { }
}