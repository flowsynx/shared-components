using FlowSynx.Connectors.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Connectors.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConnectorsManager(this IServiceCollection services)
    {
        services.AddScoped<IConnectorsManager, ConnectorsManager>();
        return services;
    }
}