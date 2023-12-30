using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text;

namespace FlowSynx.Logging.FileLogger;

[ProviderAlias("File")]
public class FileLoggerProvider : LoggerProvider
{
    private bool _terminated;
    private string _filePath;
    private bool _isActiveLogging = true;
    private readonly Dictionary<string, int> _lengths = new Dictionary<string, int>();
    private readonly ConcurrentQueue<LogMessage> _logsQueue = new ConcurrentQueue<LogMessage>();

    private void BeginFile()
    {
        if (string.IsNullOrEmpty(Settings.Path))
        {
            _isActiveLogging = false;
            return;
        }

        var directory = Path.GetDirectoryName(Settings.Path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        
        _filePath = Settings.Path;
    }

    private void WriteLogLine()
    {
        if (!_logsQueue.TryDequeue(out var logMessage)) return;

        var sb = new StringBuilder();
        sb.AppendLine(LogTemplate.Format(logMessage, OutputTemplate));
        WriteLine(sb.ToString());
    }

    private void WriteLine(string message)
    {
        if (!_isActiveLogging) return;
        File.AppendAllText(_filePath, message);
    }

    private void ThreadProc()
    {
        Task.Run(() => {

            while (!_terminated)
            {
                try
                {
                    WriteLogLine();
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

    public FileLoggerProvider(FileLoggerOptions settings)
    {
        Settings = settings;
        BeginFile();

        if (_isActiveLogging)
            ThreadProc();
    }

    public override bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None && logLevel >= Settings.MinLevel && _isActiveLogging;
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

    internal FileLoggerOptions Settings { get; private set; }
}