namespace FlowSynx.Connectors.Abstractions;

public class ConnectorOptions : Dictionary<string, object?>
{
    public void ChangeProperty(string propertyName, object propertyValue)
    {
        if (this.ContainsKey(propertyName))
        {
            this[propertyName] = propertyValue;
        }
    }
}