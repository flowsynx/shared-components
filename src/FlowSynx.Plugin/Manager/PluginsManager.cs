using EnsureThat;
using FlowSynx.Data.Extensions;
using FlowSynx.Data.Filter;
using FlowSynx.IO.Serialization;
using FlowSynx.Plugin.Abstractions;
using FlowSynx.Plugin.Exceptions;
using FlowSynx.Plugin.Manager.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Plugin.Manager;

public class PluginsManager : IPluginsManager
{
    private readonly ILogger<PluginsManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDeserializer _deserializer;
    private readonly IDataFilter _dataFilter;

    public PluginsManager(ILogger<PluginsManager> logger, IServiceProvider serviceProvider,
        IDeserializer deserializer, IDataFilter dataFilter)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));
        EnsureArg.IsNotNull(deserializer, nameof(deserializer));
        EnsureArg.IsNotNull(dataFilter, nameof(dataFilter));
        _logger = logger;
        _serviceProvider = serviceProvider;
        _deserializer = deserializer;
        _dataFilter = dataFilter;
    }

    public IEnumerable<object> List(PluginListOptions listOptions)
    {
        var plugins = Plugins().Select(plg => new PluginResponse
        {
            Id = plg.Id,
            Name = plg.Name,
            Type = plg.Type,
            Description = plg.Description
        });

        var dataFilterOptions = new DataFilterOptions
        {
            Fields = listOptions.Fields ?? Array.Empty<string>(),
            FilterExpression = listOptions.Filter ?? string.Empty,
            SortExpression = listOptions.Sort ?? string.Empty,
            CaseSensitive = listOptions.CaseSensitive ?? false,
            Limit = listOptions.Limit ?? string.Empty,
        };

        var dataTable = plugins.ToDataTable();
        var filteredData = _dataFilter.Filter(dataTable, dataFilterOptions);
        return filteredData.CreateListFromTable();
    }

    public PluginBase Get(string type)
    {
        var result = Plugins().FirstOrDefault(x => x.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

        if (result != null)
            return (PluginBase)ActivatorUtilities.CreateInstance(_serviceProvider, result.GetType());

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

    private IEnumerable<PluginBase> Plugins() => _serviceProvider.GetServices<PluginBase>();
}