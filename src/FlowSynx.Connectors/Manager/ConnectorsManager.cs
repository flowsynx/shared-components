using EnsureThat;
using FlowSynx.Connectors.Abstractions;
using FlowSynx.Data.Extensions;
using FlowSynx.Data.Filter;
using FlowSynx.IO.Serialization;
using FlowSynx.Connectors.Exceptions;
using FlowSynx.Plugin.Manager.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Connectors.Manager;

public class ConnectorsManager : IConnectorsManager
{
    private readonly ILogger<ConnectorsManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDeserializer _deserializer;
    private readonly IDataFilter _dataFilter;

    public ConnectorsManager(ILogger<ConnectorsManager> logger, IServiceProvider serviceProvider,
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

    public IEnumerable<object> List(ConnectorListOptions listOptions)
    {
        var plugins = Plugins().Select(plg => new ConnectorResponse
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

    public Connector Get(string type)
    {
        var result = Plugins().FirstOrDefault(x => x.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

        if (result != null)
            return (Connector)ActivatorUtilities.CreateInstance(_serviceProvider, result.GetType());

        _logger.LogError($"Plugin {type} could not found!");
        throw new ConnectorsManagerException(string.Format(Resources.PluginsManagerCouldNotFoumd, type));
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

    private IEnumerable<Connector> Plugins() => _serviceProvider.GetServices<Connector>();
}