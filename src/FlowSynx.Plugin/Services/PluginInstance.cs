using EnsureThat;
using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Plugin.Services;

public class PluginInstance
{
    public PluginInstance(PluginBase primaryPlugin, string entity, PluginSpecifications? specifications)
    {
        EnsureArg.IsNotNull(primaryPlugin, nameof(primaryPlugin));
        PrimaryPlugin = primaryPlugin;
        SecondaryPlugin = null;
        Entity = entity;
        Specifications = specifications;
        primaryPlugin.Specifications = specifications;
    }

    public PluginInstance(PluginBase primaryPlugin, PluginBase secondaryPlugin, string entity, PluginSpecifications? specifications)
    {
        EnsureArg.IsNotNull(primaryPlugin, nameof(primaryPlugin));
        PrimaryPlugin = primaryPlugin;
        SecondaryPlugin = secondaryPlugin;
        Entity = entity;
        Specifications = specifications;
        primaryPlugin.Specifications = specifications;
    }

    public string Entity { get; set; }
    public PluginBase PrimaryPlugin { get; }
    public PluginBase? SecondaryPlugin { get; }
    public PluginSpecifications? Specifications { get; }

    public Task Initialize()
    {
        PrimaryPlugin.Initialize();
        SecondaryPlugin?.Initialize();
        return Task.CompletedTask;
    }
}