namespace FlowSynx.Connectors.Abstractions;

public class Context
{
    public Context(Connector currentConnector, Connector? nextConnector, string entity)
    {
        CurrentConnector = currentConnector;
        NextConnector = nextConnector;
        Entity = entity;
    }

    public Connector CurrentConnector { get; }
    public Connector? NextConnector { get; }
    public string Entity { get; set; }
}