using FlowSynx.Plugin.Abstractions;
using FlowSynx.Plugin.Manager;
using FlowSynx.Plugin.Manager.Filters;
using FlowSynx.Plugin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Plugin.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPluginManager(this IServiceCollection services)
    {
        services
            .AddScoped<IPluginFilter, PluginFilter>()
            .AddScoped<IPluginsManager, PluginsManager>()
            .AddScoped<IPluginService, PluginService>();

        return services;
    }
}