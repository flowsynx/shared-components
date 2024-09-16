﻿using FlowSynx.Plugin.Manager;
using FlowSynx.Plugin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Plugin.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPluginManager(this IServiceCollection services)
    {
        services.AddScoped<IPluginsManager, PluginsManager>();
        return services;
    }

    public static IServiceCollection AddPluginService(this IServiceCollection services)
    {
        services.AddScoped<IPluginService, PluginService>();

        return services;
    }
}