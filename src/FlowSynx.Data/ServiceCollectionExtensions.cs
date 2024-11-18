using FlowSynx.Data.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowSynxData(this IServiceCollection services)
    {
        services.AddScoped<IDataService, DataService>();
        return services;
    }
}