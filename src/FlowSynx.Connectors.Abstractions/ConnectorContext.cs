namespace FlowSynx.Connectors.Abstractions;

public class ConnectorContext
{
    public ConnectorContext(Connector connector, Context context)
    {
        Connector = connector;
        Context = context;
    }

    public Connector Connector { get; }
    public Context Context { get; set; }
}