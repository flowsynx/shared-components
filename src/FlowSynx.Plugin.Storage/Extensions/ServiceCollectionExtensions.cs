using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Plugin.Storage.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageFilter(this IServiceCollection services)
    {
        services.AddScoped<IStorageFilter, StorageFilter>();

        return services;
    }
}