using FlowSynx.Plugin.Abstractions.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Plugin.Abstractions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatFilter(this IServiceCollection services)
    {
        services.AddScoped<IDataFilter, DataFilter>();
        return services;
    }
}