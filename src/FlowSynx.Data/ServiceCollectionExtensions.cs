using FlowSynx.Data.DataTableQuery.Queries;
using FlowSynx.Data.SqlQuery.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowSynxDataService(this IServiceCollection services)
    {
        services.AddScoped<IDataTableService, DataTableService>();
        services.AddScoped<ISqlService, SqlService>();

        return services;
    }
}