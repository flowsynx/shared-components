using EnsureThat;
using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Plugin.Services;

public class PluginInstance
{
    public PluginInstance(IPlugin plugin, string entity, PluginSpecifications? specifications)
    {
        EnsureArg.IsNotNull(plugin, nameof(plugin));
        Plugin = plugin;
        Entity = entity;
        Specifications = specifications;
        plugin.Specifications = specifications;
    }

    public string Entity { get; set; }
    public IPlugin Plugin { get; }
    public PluginSpecifications? Specifications { get; }

    public Task Initialize()
    {
        return Plugin.Initialize();
    }
}