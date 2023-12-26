using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace FlowSynx.Logging;

public abstract class LoggerProvider : ILoggerProvider, IDisposable
{
    private readonly ConcurrentDictionary<string, Logger> _loggers = new ConcurrentDictionary<string, Logger>();

    ILogger ILoggerProvider.CreateLogger(string category)
    {
        return _loggers.GetOrAdd(category, (cat) => new Logger(this, cat));
    }

    void IDisposable.Dispose()
    {
        if (IsDisposed) return;
        IsDisposed = true;
        GC.SuppressFinalize(this);
    }

    protected LoggerProvider()
    {
    }
    
    public abstract bool IsEnabled(LogLevel logLevel);
    public abstract void WriteLog(LogMessage info);
    public abstract string? OutputTemplate {get; set; }
    public bool IsDisposed { get; protected set; }
}
