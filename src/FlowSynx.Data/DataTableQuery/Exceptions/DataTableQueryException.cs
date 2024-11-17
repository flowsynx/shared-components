using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Data.DataTableQuery.Extensions.Exceptions;

public class DataTableQueryException : FlowSynxException
{
    public DataTableQueryException(string message) : base(message) { }
    public DataTableQueryException(string message, Exception inner) : base(message, inner) { }
}