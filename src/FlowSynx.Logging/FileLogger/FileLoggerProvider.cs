using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text;

namespace FlowSynx.Logging.FileLogger;

[ProviderAlias("File")]
public class FileLoggerProvider : LoggerProvider
{
    private bool _terminated;
    private int _counter = 0;
    private string _filePath;
    private readonly Dictionary<string, int> _lengths = new Dictionary<string, int>();
    private readonly ConcurrentQueue<LogMessage> _logsQueue = new ConcurrentQueue<LogMessage>();

    private void ApplyRetainPolicy()
    {
        try
        {
            var fileList = new DirectoryInfo(Settings.Path)
            .GetFiles("*.log", SearchOption.TopDirectoryOnly)
            .OrderBy(fi => fi.CreationTime)
            .ToList();

            while (fileList.Count >= Settings.RetainPolicyFileCount)
            {
                var fileInfo = fileList.First();
                fileInfo.Delete();
                fileList.Remove(fileInfo);
            }
        }
        catch
        {
            // ignored
        }
    }

    private void WriteLine(string message)
    {
        _counter++;
        if (_counter % 100 == 0)
        {
            var fi = new FileInfo(_filePath);
            if (fi.Length > (1024 * 1024 * Settings.MaxFileSizeInMb))
            {
                BeginFile();
            }
        }

        File.AppendAllText(_filePath, message);
    }

    private void BeginFile()
    {
        Directory.CreateDirectory(Settings.Path);
        _filePath = Path.Combine(Settings.Path, "Log-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".log");
        ApplyRetainPolicy();
    }

    private void WriteLogLine()
    {
        if (!_logsQueue.TryDequeue(out var logMessage)) return;

        var sb = new StringBuilder();
        sb.AppendLine(LogTemplate.Format(logMessage, OutputTemplate));
        WriteLine(sb.ToString());
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

    internal FileLoggerOptions Settings { get; private set; }
}