namespace FlowSynx.Plugin.Abstractions;

public class PluginContext
{
    public PluginContext(PluginBase invokePlugin, PluginBase? inferiorPlugin, string entity)
    {
        InvokePlugin = invokePlugin;
        InferiorPlugin = inferiorPlugin;
        Entity = entity;
    }

    public PluginBase InvokePlugin { get; }
    public PluginBase? InferiorPlugin { get; }
    public string Entity { get; set; }
}