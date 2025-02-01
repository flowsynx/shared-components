using FlowSynx.Data;

namespace FlowSynx.Connectors.Abstractions;

public class Context: ICloneable
{
    public Context()
    {
        Options = new ConnectorOptions();
        Data = new List<object>();
    }

    public Context(ConnectorOptions options)
    {
        Options = options;
        Data = new List<object>();
    }

    public Context(ConnectorOptions options, List<object>? data)
    {
        Options = options;
        Data = data;
    }

    public ConnectorOptions Options { get; set; }
    public List<object>? Data { get; set; }

    public object Clone()
    {
        var clone = (Context)MemberwiseClone();
        return clone;
    }
}