using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace FlowSynx.Logging.InMemory;

[ProviderAlias("InMemory")]
public class InMemoryLoggerProvider : LoggerProvider
{
    private bool _terminated;
    private readonly Dictionary<string, int> _lengths = new Dictionary<string, int>();
    private readonly ConcurrentQueue<LogMessage> _logsQueue = new ConcurrentQueue<LogMessage>();
    private readonly List<LogMessage> _logLines = new List<LogMessage>();

    public IEnumerable<LogMessage> RecordedLogs => this._logLines.AsReadOnly();

    private void WriteLine()
    {
        if (!_logsQueue.TryDequeue(out var logMessage)) return;
        _logLines.Add(logMessage);
    }

    private void ThreadProc()
    {
        Task.Run(() => {

            while (!_terminated)
            {
                try
                {
                    WriteLine();
                    System.Threading.Thread.Sleep(100);
                }
                catch
                {
                    // ignored
                }
            }

        });
    }

    protected void Dispose()
    {
        _terminated = true;
    }

    public InMemoryLoggerProvider(InMemoryLoggerOptions settings)
    {
        Settings = settings;
        ThreadProc();
    }

    public override bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None && logLevel >= Settings.MinLevel;
    }

    public override void WriteLog(LogMessage logMessage)
    {
        _logsQueue.Enqueue(logMessage);
    }

    public override string OutputTemplate
    {
        get => Settings.OutputTemplate;
        set => Settings.OutputTemplate = value;
    }

    internal InMemoryLoggerOptions Settings { get; private set; }
}