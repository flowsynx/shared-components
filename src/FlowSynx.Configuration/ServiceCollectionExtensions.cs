using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowSynxConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IConfigurationManager, ConfigurationManager>();

        return services;
    }
}