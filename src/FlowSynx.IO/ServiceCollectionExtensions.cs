using FlowSynx.IO.Cache;
using FlowSynx.IO.Compression;
using FlowSynx.IO.Exceptions;
using FlowSynx.IO.FileSystem;
using FlowSynx.IO.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace FlowSynx.IO;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
    {
        services
            .AddTransient<ISerializer, JsonSerializer>()
            .AddTransient<IDeserializer, JsonDeserializer>();

        return services;
    }

    public static IServiceCollection AddFileSystem(this IServiceCollection services)
    {
        services
            .AddTransient<IFileReader, FileReader>()
            .AddTransient<IFileWriter, FileWriter>();
        return services;
    }

    public static IServiceCollection AddCache<TKey, TValue>(this IServiceCollection services) 
        where TKey : notnull
    {
        services.AddSingleton(typeof(ICache<TKey, TValue>), typeof(FlowSynxCache<TKey, TValue>));
        return services;
    }

    public static IServiceCollection AddCompressions(this IServiceCollection services)
    {
        services.AddScoped<ZipCompression>();
        services.AddScoped<GZipCompression>();
        services.AddScoped<TarCompression>();

        services.AddTransient<Func<CompressType, ICompression>>(compressionProvider => key =>
        {
            return key switch
            {
                CompressType.Zip => compressionProvider.GetService<ZipCompression>() 
                                    ?? throw new CompressionException(Resources.CompressTypeNotFound),
                CompressType.GZip => compressionProvider.GetService<GZipCompression>() 
                                     ?? throw new CompressionException(Resources.CompressTypeNotFound),
                CompressType.Tar => compressionProvider.GetService<TarCompression>() 
                                    ?? throw new CompressionException(Resources.CompressTypeNotFound),
                _ => throw new CompressionException(Resources.CompressTypeNotSupported)
            };
        });
        return services;
    }
}