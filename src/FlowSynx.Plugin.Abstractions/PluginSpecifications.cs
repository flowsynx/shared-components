namespace FlowSynx.Plugin.Abstractions;

public class PluginSpecifications : Dictionary<string, string?>
{
    public PluginSpecifications() : base(StringComparer.OrdinalIgnoreCase)
    {

    }
}