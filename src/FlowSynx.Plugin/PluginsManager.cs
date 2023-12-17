using EnsureThat;
using FlowSynx.Plugin.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Plugin;

public class PluginsManager : IPluginsManager
{
    private readonly ILogger<PluginsManager> _logger;
    private readonly IServiceProvider _serviceProvider;

    public PluginsManager(ILogger<PluginsManager> logger, IServiceProvider serviceProvider)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public IPlugin GetPlugin(string type)
    {
        var result = Plugins().FirstOrDefault(x => x.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

        if (result != null)
            return (IPlugin)ActivatorUtilities.CreateInstance(_serviceProvider, result.GetType());
        
        _logger.LogError($"Plugin {type} could not found!");
        throw new PluginManagerException(string.Format(Resources.PluginsManagerCouldNotFoumd, type));
    }
    
    public bool IsExist(string type)
    {
        var result = Plugins().FirstOrDefault(x => x.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
        if (result != null) return true;

        _logger.LogError($"The specified plugin '{type}' not found!");
        return false;
    }

    public IEnumerable<IPlugin> Plugins() => _serviceProvider.GetServices<IPlugin>();

    public IEnumerable<IPlugin> Plugins(PluginNamespace @namespace) => Plugins().Where(x => x.Namespace == @namespace);
}