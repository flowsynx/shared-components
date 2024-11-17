using EnsureThat;
using FlowSynx.Data.DataTableQuery.Extensions;
using FlowSynx.Data.DataTableQuery.Queries;
using FlowSynx.Data.DataTableQuery.Queries.Select;
using FlowSynx.IO.Serialization;
using FlowSynx.Logging.Extensions;
using FlowSynx.Logging.InMemory;
using FlowSynx.Logging.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging;

public class LogManager : ILogManager
{
    private readonly ILogger<LogManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDeserializer _deserializer;
    private readonly IDataTableService _dataTableService;
    private readonly InMemoryLoggerProvider? _inMemoryLogger;

    public LogManager(ILogger<LogManager> logger, IServiceProvider serviceProvider,
        IDeserializer deserializer, IDataTableService dataTableService)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));
        EnsureArg.IsNotNull(deserializer, nameof(deserializer));
        EnsureArg.IsNotNull(dataTableService, nameof(dataTableService));
        _logger = logger;
        _serviceProvider = serviceProvider;
        _deserializer = deserializer;
        _dataTableService = dataTableService;

        var loggerProviders = serviceProvider.GetServices<ILoggerProvider>();
        _inMemoryLogger = GeInMemoryLoggerProvider(loggerProviders);
    }

    public IEnumerable<object> List(LogListOptions listOptions)
    {
        var dataTable = Logs().ToDataTable();
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

    private IEnumerable<LogMessageResponse> Logs()
    {
        EnsureArg.IsNotNull(_inMemoryLogger, nameof(_inMemoryLogger));
        return _inMemoryLogger.RecordedLogs.Select(log => new LogMessageResponse
        {
            UserName = log.UserName,
            Machine = log.Machine,
            TimeStamp = log.TimeStamp,
            Message = log.Message,
            Level = log.Level.ToFlowSynxLogLevel().ToString().ToUpper()
        });
    }

    private InMemoryLoggerProvider? GeInMemoryLoggerProvider(IEnumerable<ILoggerProvider> providers)
    {
        foreach (var provider in providers)
        {
            if (provider is InMemoryLoggerProvider loggerProvider)
            {
                return loggerProvider;
            }
        }

        return null;
    }
}