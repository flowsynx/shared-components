using FlowSynx.Data.Sql.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Data.Sql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowSynxSqlBuilder(this IServiceCollection services)
    {
        services.AddScoped<ISqlBuilder, SqlBuilder>();
        return services;
    }
}