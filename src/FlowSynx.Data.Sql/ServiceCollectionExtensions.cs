using FlowSynx.Data.SqlQuery.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Data.Sql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowSynxDataSql(this IServiceCollection services)
    {
        services.AddScoped<ISqlService, SqlService>();
        return services;
    }
}