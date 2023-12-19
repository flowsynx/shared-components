using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Environment;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowSynxEnvironmentManager(this IServiceCollection services)
    {
        services.AddTransient<IEnvironmentManager, EnvironmentManager>();
        return services;
    }

    public static IServiceCollection AddFlowSynxEndpoint(this IServiceCollection services)
    {
        services.AddTransient<IEndpoint, DefaultEndpoint>();
        return services;
    }

    public static IServiceCollection AddFlowSynxOperatingSystemInfo(this IServiceCollection services)
    {
        services.AddTransient<IOperatingSystemInfo, OperatingSystemInfo>();
        return services;
    }
}