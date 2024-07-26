using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging.InMemory;

public class InMemoryLoggerOptions
{
    public string OutputTemplate { get; set; } = string.Empty;
    public LogLevel MinLevel { get; set; } = LogLevel.Information;
}