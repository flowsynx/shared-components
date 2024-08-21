using EnsureThat;
using FlowSynx.Plugin.Abstractions;
using FlowSynx.Plugin.Exceptions;
using FlowSynx.Plugin.Manager.Filters;
using FlowSynx.Plugin.Manager.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Plugin.Manager;

public class PluginsManager : IPluginsManager
{
    private readonly ILogger<PluginsManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IPluginFilter _pluginFilter;

    public PluginsManager(ILogger<PluginsManager> logger, IServiceProvider serviceProvider,
        IPluginFilter pluginFilter)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));
        _logger = logger;
        _serviceProvider = serviceProvider;
        _pluginFilter = pluginFilter;
    }

    public IEnumerable<IPlugin> List(PluginSearchOptions searchOptions, PluginListOptions listOptions)
    {
        return _pluginFilter.FilterPluginsList(Plugins(), searchOptions, listOptions);
    }

    public IPlugin Get(string type)
    {
        var result = Plugins().FirstOrDefault(x => x.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

        if (result != null)
            return (IPlugin)ActivatorUtilities.CreateInstance(_serviceProvider, result.GetType());

        _logger.LogError($"Plugin {type} could not found!");
        throw new PluginManagerException(string.Format(Resources.PluginsManagerCouldNotFoumd, type));
    }

    public bool IsExist(string type)
    {
        try
        {
            Get(type);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private IEnumerable<IPlugin> Plugins() => _serviceProvider.GetServices<IPlugin>();
}