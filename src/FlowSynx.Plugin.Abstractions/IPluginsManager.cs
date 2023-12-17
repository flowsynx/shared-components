namespace FlowSynx.Plugin.Abstractions;

public interface IPluginsManager
{
    IEnumerable<IPlugin> Plugins();
    IEnumerable<IPlugin> Plugins(PluginNamespace @namespace);
    IPlugin GetPlugin(string type);
    bool IsExist(string type);
}