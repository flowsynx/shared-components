using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging.FileLogger;

public class FileLoggerOptions
{
    private string? _path;

    public string OutputTemplate { get; set; } = string.Empty;
    public LogLevel MinLevel { get; set; } = LogLevel.Information;

    public string? Path
    {
        get => string.IsNullOrWhiteSpace(_path) ? System.IO.Path.GetDirectoryName(AppContext.BaseDirectory) : _path;
        set => _path = value;
    }
}