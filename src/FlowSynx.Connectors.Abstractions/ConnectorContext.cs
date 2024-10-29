namespace FlowSynx.Connectors.Abstractions;

public class ConnectorContext
{
    public Connector Current { get; set; }
    public ConnectorContext? Next { get; set; }

    public ConnectorContext(Connector current)
    {
        Current = current;
        Next = null;
    }
}