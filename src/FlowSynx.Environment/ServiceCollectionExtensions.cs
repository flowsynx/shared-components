using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Environment;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEnvironmentManager(this IServiceCollection services)
    {
        services.AddTransient<IEnvironmentManager, EnvironmentManager>();
        return services;
    }

    public static IServiceCollection AddEndpoint(this IServiceCollection services)
    {
        services.AddTransient<IEndpoint, DefaultEndpoint>();
        return services;
    }

    public static IServiceCollection AddOperatingSystemInfo(this IServiceCollection services)
    {
        services.AddTransient<IOperatingSystemInfo, OperatingSystemInfo>();
        return services;
    }

    public static IServiceCollection AddSystemClock(this IServiceCollection services)
    {
        services.AddTransient<ISystemClock, SystemClock>();
        return services;
    }

    public static IServiceCollection AddProcessHandler(this IServiceCollection services)
    {
        services.AddTransient<IProcessHandler, ProcessHandler>();
        return services;
    }
}