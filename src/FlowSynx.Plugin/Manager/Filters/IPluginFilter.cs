using FlowSynx.Plugin.Abstractions;
using FlowSynx.Plugin.Manager.Options;

namespace FlowSynx.Plugin.Manager.Filters;

public interface IPluginFilter
{
    IEnumerable<IPlugin> FilterPluginsList(IEnumerable<IPlugin> plugins,
        PluginSearchOptions searchOptions, PluginListOptions listOptions);
}