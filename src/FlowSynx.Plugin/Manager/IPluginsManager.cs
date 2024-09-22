using FlowSynx.Plugin.Abstractions;
using FlowSynx.Plugin.Manager.Options;

namespace FlowSynx.Plugin.Manager;

public interface IPluginsManager
{
    IEnumerable<object> List(PluginListOptions listOptions);
    PluginBase Get(string type);
    bool IsExist(string type);
}