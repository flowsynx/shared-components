using EnsureThat;
using FlowSynx.Data.Extensions;
using FlowSynx.Data.Filter;
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
    private readonly IDataFilter _dataFilter;
    private readonly InMemoryLoggerProvider? _inMemoryLogger;

    public LogManager(ILogger<LogManager> logger, IServiceProvider serviceProvider,
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

        var loggerProviders = serviceProvider.GetServices<ILoggerProvider>();
        _inMemoryLogger = GeInMemoryLoggerProvider(loggerProviders);
    }

    public IEnumerable<object> List(LogListOptions listOptions)
    {
        var logs = Logs();
        var dataFilterOptions = new DataFilterOptions
        {
            Fields = listOptions.Fields ?? Array.Empty<string>(),
            FilterExpression = listOptions.Filter ?? string.Empty,
            SortExpression = listOptions.Sort ?? string.Empty,
            CaseSensitive = listOptions.CaseSensitive ?? false,
            Limit = listOptions.Limit ?? string.Empty,
        };

        var logsList = logs.ToList();
        var dataTable = logsList.ToDataTable();
        var filteredData = _dataFilter.Filter(dataTable, dataFilterOptions);
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

    private string[] DeserializeToStringArray(string? fields)
    {
        string[] result = [];
        if (!string.IsNullOrEmpty(fields))
        {
            result = _deserializer.Deserialize<string[]>(fields);
        }

        return result;
    }
}