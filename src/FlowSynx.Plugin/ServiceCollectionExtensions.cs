using FlowSynx.Plugin.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Plugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPluginManager(this IServiceCollection services)
    {
        services.AddScoped<IPluginsManager, PluginsManager>();
        return services;
    }
}