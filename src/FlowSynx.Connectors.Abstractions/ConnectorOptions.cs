namespace FlowSynx.Connectors.Abstractions;

public class ConnectorOptions : Dictionary<string, object?>, ICloneable
{
    public void ChangeProperty(string propertyName, object propertyValue)
    {
        if (this.ContainsKey(propertyName))
        {
            this[propertyName] = propertyValue;
        }
    }

    public object Clone()
    {
        var clone = (ConnectorOptions)MemberwiseClone();
        return clone;
    }
}