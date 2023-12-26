using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging;

internal class Logger : ILogger
{
    public LoggerProvider Provider { get; }
    public string Category { get; }

    public Logger(LoggerProvider provider, string category)
    {
        Provider = provider;
        Category = category;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return Provider.IsEnabled(logLevel);
    }

    void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var message = new LogMessage
        {
            Category = Category,
            Level = logLevel,
            Message = FormatState(state, exception, formatter),
            EventId = eventId
        };
        Provider.WriteLog(message);
    }

    protected virtual string FormatState<TState>(TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        return formatter(state, exception);
    }
}