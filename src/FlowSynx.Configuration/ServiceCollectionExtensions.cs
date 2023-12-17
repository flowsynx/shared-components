using FlowSynx.IO.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowSynxConfiguration(this IServiceCollection services)
    {
        services
            .AddTransient<ISerializer, JsonSerializer>()
            .AddTransient<IDeserializer, JsonDeserializer>()
            .AddScoped<IConfigurationManager, ConfigurationManager>();

        return services;
    }
}