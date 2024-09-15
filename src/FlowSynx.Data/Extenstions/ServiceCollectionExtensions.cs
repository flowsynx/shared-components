using FlowSynx.Data.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatFilter(this IServiceCollection services)
    {
        services.AddScoped<IDataFilter, DataFilter>();
        return services;
    }
}