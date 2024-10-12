using FlowSynx.Connectors.Abstractions;

namespace FlowSynx.Connectors.Manager;

public interface IConnectorsManager
{
    IEnumerable<object> List(ConnectorListOptions listOptions);
    Connector Get(string type);
    bool IsExist(string type);
}