using FlowSynx.Logging.ConsoleLogger;
using FlowSynx.Logging.FileLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging;

public static class LoggerExtensions
{
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.AddFileLogger(configure =>
        {
            configure.OutputTemplate = "[time={timestamp} | level={level}] message=\"{message}\"";
            configure.MinLevel = LogLevel.Information;
        });
        return builder;
    }

    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>(
            (provider) => {
                var options = new FileLoggerOptions();
                configure(options);
                return new FileLoggerProvider(options);
            }
        ));

        return builder;
    }

    public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.AddConsoleLogger(configure =>
        {
            configure.OutputTemplate = "[time={timestamp} | level={level}] message=\"{message}\"";
            configure.MinLevel = LogLevel.Information;
        });
        return builder;
    }

    public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder builder, Action<ConsoleLoggerOptions> configure)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider, ConsoleLoggerProvider>(
            (provider) => {
                var options = new ConsoleLoggerOptions();
                configure(options);
                return new ConsoleLoggerProvider(options);
            }
        ));

        return builder;
    }
}