using EnsureThat;
using FlowSynx.Data.Extensions;
using FlowSynx.Data.Queries;
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
    private readonly IDataService _dataService;
    private readonly InMemoryLoggerProvider? _inMemoryLogger;

    public LogManager(ILogger<LogManager> logger, IServiceProvider serviceProvider,
        IDeserializer deserializer, IDataService dataService)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));
        EnsureArg.IsNotNull(deserializer, nameof(deserializer));
        EnsureArg.IsNotNull(dataService, nameof(dataService));
        _logger = logger;
        _serviceProvider = serviceProvider;
        _deserializer = deserializer;
        _dataService = dataService;

        var loggerProviders = serviceProvider.GetServices<ILoggerProvider>();
        _inMemoryLogger = GeInMemoryLoggerProvider(loggerProviders);
    }

    public IEnumerable<object> List(LogListOptions listOptions)
    {
        var dataTable = Logs().ListToDataTable();
        var selectDataOption = new SelectDataOption()
        {
            Fields = listOptions.Fields,
            Filters = listOptions.Filters,
            Sorts = listOptions.Sorts,
            Paging = listOptions.Paging,
            CaseSensitive = listOptions.CaseSensitive,
        };

        var filteredData = _dataService.Select(dataTable, selectDataOption);
        return filteredData.DataTableToList();
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