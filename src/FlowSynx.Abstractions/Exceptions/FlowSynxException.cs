namespace FlowSynx.Abstractions.Exceptions;

public class FlowSynxException : Exception
{
    public FlowSynxException(string message)
        : base(message)
    {
    }

    public FlowSynxException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}