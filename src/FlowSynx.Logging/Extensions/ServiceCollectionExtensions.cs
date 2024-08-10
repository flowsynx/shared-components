using FlowSynx.Logging.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Logging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLoggerFilter(this IServiceCollection services)
    {
        services.AddScoped<ILogFilter, LogFilter>();
        return services;
    }
}