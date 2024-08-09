using FlowSynx.Plugin.Abstractions;
using FlowSynx.Plugin.Options;

namespace FlowSynx.Plugin.Filters;

public interface IPluginFilter
{
    IEnumerable<IPlugin> FilterPluginsList(IEnumerable<IPlugin> plugins,
        PluginSearchOptions searchOptions, PluginListOptions listOptions);
}