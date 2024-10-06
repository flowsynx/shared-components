using FlowSynx.Plugin.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Plugin.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPluginManager(this IServiceCollection services)
    {
        services.AddScoped<IPluginsManager, PluginsManager>();
        return services;
    }
}