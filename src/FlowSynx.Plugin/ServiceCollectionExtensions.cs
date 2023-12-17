using FlowSynx.Plugin.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Plugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowSynxPluginManager(this IServiceCollection services)
    {
        services.AddScoped<IPluginsManager, PluginsManager>();
        return services;
    }
}