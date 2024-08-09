using FlowSynx.Plugin.Storage.Check;
using FlowSynx.Plugin.Storage.Compress;
using FlowSynx.Plugin.Storage.Copy;
using FlowSynx.Plugin.Storage.Filter;
using FlowSynx.Plugin.Storage.Move;
using FlowSynx.Plugin.Storage.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Plugin.Storage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageService(this IServiceCollection services)
    {
        services
            .AddScoped<IStorageFilter, StorageFilter>()
            .AddScoped<IStorageEntityCopier, StorageEntityCopier>()
            .AddScoped<IStorageEntityMover, StorageEntityMover>()
            .AddScoped<IStorageEntityChecker, StorageEntityChecker>()
            .AddScoped<IStorageEntityCompress, StorageEntityCompress>()
            .AddScoped<IStorageService, StorageService>();

        return services;
    }
}