namespace FlowSynx.Connectors.Abstractions;

public class Context
{
    public Context(string entity)
    {
        Entity = entity;
        Connector = null;
    }

    public Context(string entity, Connector connector)
    {
        Entity = entity;
        Connector = connector;
    }

    public string Entity { get; set; }
    public Connector? Connector { get; }
}