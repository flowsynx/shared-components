namespace FlowSynx.Plugin.Abstractions;

public class PluginFilters : Dictionary<string, object?>
{
    public void ChangeProperty(string propertyName, object propertyValue)
    {
        if (this.ContainsKey(propertyName))
        {
            this[propertyName] = propertyValue;
        }
    }
}