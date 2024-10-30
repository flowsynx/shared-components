namespace FlowSynx.Connectors.Abstractions;

public class Context: ICloneable
{
    public Context()
    {
        Options = new ConnectorOptions();
        ConnectorContext = null;
    }

    public Context(ConnectorOptions options)
    {
        Options = options;
        ConnectorContext = null;
    }

    public Context(ConnectorOptions options, ConnectorContext? connector)
    {
        Options = options;
        ConnectorContext = connector;
    }

    public ConnectorOptions Options { get; set; }
    public ConnectorContext? ConnectorContext { get; }

    public object Clone()
    {
        var clone = (Context)MemberwiseClone();
        return clone;
    }
}