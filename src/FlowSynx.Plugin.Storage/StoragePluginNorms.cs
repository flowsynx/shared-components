using EnsureThat;
using FlowSynx.Plugin.Storage.Abstractions;

namespace FlowSynx.Plugin.Storage;

public class StoragePluginNorms
{
    public StoragePluginNorms(IStoragePlugin plugin, Dictionary<string, string?>? specifications, string path)
    {
        EnsureArg.IsNotNull(plugin, nameof(plugin));
        Plugin = plugin;
        Specifications = specifications;
        plugin.Specifications = Specifications;
        Path = path;
    }

    public string Path { get; set; }
    public IStoragePlugin Plugin { get; }
    public Dictionary<string, string?>? Specifications { get; }

    public Task Initialize()
    {
        return Plugin.Initialize();
    }
}