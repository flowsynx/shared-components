using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Data.Exceptions;

public class DataException : FlowSynxException
{
    public DataException(string message) : base(message) { }
    public DataException(string message, Exception inner) : base(message, inner) { }
}