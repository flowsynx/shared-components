using FlowSynx.IO.Compression;
using FlowSynx.IO.FileSystem;
using FlowSynx.IO.Serialization;
using Microsoft.Extensions.DependencyInjection;

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

    public static IServiceCollection AddCompression(this IServiceCollection services)
    {
        services
            .AddTransient<IZipFile, ZipFile>()
            .AddTransient<IGZipFile, GZipFile>();

        return services;
    }
}