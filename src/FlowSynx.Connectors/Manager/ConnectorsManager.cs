using EnsureThat;
using FlowSynx.Connectors.Abstractions;
using FlowSynx.Connectors.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FlowSynx.Data.DataTableQuery.Extensions;
using FlowSynx.Data.DataTableQuery.Queries;
using FlowSynx.Data.DataTableQuery.Queries.Select;

namespace FlowSynx.Connectors.Manager;

public class ConnectorsManager : IConnectorsManager
{
    private readonly ILogger<ConnectorsManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDataTableService _dataTableService;

    public ConnectorsManager(ILogger<ConnectorsManager> logger, IServiceProvider serviceProvider,
        IDataTableService dataTableService)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));
        EnsureArg.IsNotNull(dataTableService, nameof(dataTableService));
        _logger = logger;
        _serviceProvider = serviceProvider;
        _dataTableService = dataTableService;
    }

    public IEnumerable<object> List(ConnectorListOptions listOptions)
    {
        var connectors = Connectors().Select(plg => new ConnectorResponse
        {
            Id = plg.Id,
            Name = plg.Name,
            Type = plg.Type,
            Description = plg.Description
        });

        var dataTable = connectors.ToDataTable();
        var selectDataTableOption = new SelectDataTableOption()
        {
            Fields = listOptions.Fields,
            Filters = listOptions.Filters,
            Sorts = listOptions.Sorts,
            Paging = listOptions.Paging,
            CaseSensitive = listOptions.CaseSensitive,
        };
        
        var filteredData = _dataTableService.Select(dataTable, selectDataTableOption);
        return filteredData.CreateListFromTable();
    }

    public Connector Get(string type)
    {
        var result = Connectors().FirstOrDefault(x => x.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

        if (result != null)
            return (Connector)ActivatorUtilities.CreateInstance(_serviceProvider, result.GetType());

        _logger.LogError($"Connector {type} could not found!");
        throw new ConnectorsManagerException(string.Format(Resources.ConnectorsManagerCouldNotFoumd, type));
    }

    public bool IsExist(string type)
    {
        try
        {
            Get(type);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private IEnumerable<Connector> Connectors() => _serviceProvider.GetServices<Connector>();
}