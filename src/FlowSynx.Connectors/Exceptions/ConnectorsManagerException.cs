using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Connectors.Exceptions;

public class ConnectorsManagerException : FlowSynxException
{
    public ConnectorsManagerException(string message) : base(message) { }
    public ConnectorsManagerException(string message, Exception inner) : base(message, inner) { }
}