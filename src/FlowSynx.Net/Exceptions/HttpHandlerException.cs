namespace FlowSynx.Net.Exceptions;

public class RequestServiceException : Exception
{
    public RequestServiceException(string message) : base(message) { }
    public RequestServiceException(string message, Exception inner) : base(message, inner) { }
}