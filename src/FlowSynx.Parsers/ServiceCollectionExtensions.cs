using FlowSynx.Parsers.Date;
using FlowSynx.Parsers.Size;
using FlowSynx.Parsers.Sort;
using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Parsers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddParsers(this IServiceCollection services)
    {
        services.AddScoped<IDateParser, DateParser>();
        services.AddScoped<ISizeParser, SizeParser>();
        services.AddScoped<ISortParser, SortParser>();
        return services;
    }
}