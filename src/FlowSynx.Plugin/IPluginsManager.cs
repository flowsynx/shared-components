using FlowSynx.Plugin.Abstractions;
using FlowSynx.Plugin.Options;

namespace FlowSynx.Plugin;

public interface IPluginsManager
{
    IEnumerable<IPlugin> List(PluginSearchOptions searchOptions, PluginListOptions listOptions);
    
    IPlugin Get(string type);

    bool IsExist(string type);
}