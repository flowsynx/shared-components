﻿using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging.ConsoleLogger;

public class ConsoleLoggerOptions
{
    public string OutputTemplate { get; set; } = string.Empty;
    public LogLevel MinLevel { get; set; } = LogLevel.Information;
}