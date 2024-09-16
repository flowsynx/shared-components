using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Logging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLoggerManager(this IServiceCollection services)
    {
        services.AddScoped<ILogManager, LogManager>();
        return services;
    }
}