﻿using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IConfigurationManager, ConfigurationManager>();
        return services;
    }
}