using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace FlowSynx.Logging.ConsoleLogger;

[ProviderAlias("Console")]
public class ConsoleLoggerProvider : LoggerProvider
{
    private bool _terminated;
    private readonly Dictionary<string, int> _lengths = new Dictionary<string, int>();
    private readonly ConcurrentQueue<LogMessage> _logsQueue = new ConcurrentQueue<LogMessage>();
    private Dictionary<LogLevel, ConsoleColor> ColorMap { get; set; } = new()
    {
        [LogLevel.Trace] = ConsoleColor.DarkMagenta,
        [LogLevel.Debug] = ConsoleColor.DarkCyan,
        [LogLevel.Information] = Console.ForegroundColor,
        [LogLevel.Warning] = ConsoleColor.DarkYellow,
        [LogLevel.Error] = ConsoleColor.DarkRed,
        [LogLevel.Critical] = ConsoleColor.Red
    };

    private void WriteLine()
    {
        if (!_logsQueue.TryDequeue(out var logMessage)) return;

        ConsoleColor originalColor = Console.ForegroundColor;
        try
        {
            Console.ForegroundColor = ColorMap[logMessage.Level];
            Console.WriteLine(LogTemplate.Format(logMessage, OutputTemplate));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            Console.ForegroundColor = originalColor;
        }
    }
    
    private void ThreadProc()
    {
        Task.Run(() =>
        {

            while (!_terminated)
            {
                try
                {
                    WriteLine();
                    Thread.Sleep(100);
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
    
    public ConsoleLoggerProvider(ConsoleLoggerOptions settings)
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

    internal ConsoleLoggerOptions Settings { get; private set; }
}