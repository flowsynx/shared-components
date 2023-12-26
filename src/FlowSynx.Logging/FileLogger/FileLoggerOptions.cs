using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging.FileLogger;

public class FileLoggerOptions
{
    private string? _path;
    private int _fMaxFileSizeInMb;
    private int _fRetainPolicyFileCount;

    public string OutputTemplate { get; set; } = string.Empty;
    public LogLevel MinLevel { get; set; } = LogLevel.Information;

    public string? Path
    {
        get => string.IsNullOrWhiteSpace(_path) ? System.IO.Path.GetDirectoryName(AppContext.BaseDirectory) : _path;
        set => _path = value;
    }

    public int MaxFileSizeInMb
    {
        get => _fMaxFileSizeInMb > 0 ? _fMaxFileSizeInMb : 2;
        set => _fMaxFileSizeInMb = value;
    }

    public int RetainPolicyFileCount
    {
        get => _fRetainPolicyFileCount < 5 ? 5 : _fRetainPolicyFileCount;
        set => _fRetainPolicyFileCount = value;
    }
}