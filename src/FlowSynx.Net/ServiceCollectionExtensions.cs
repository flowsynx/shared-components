using Microsoft.Extensions.DependencyInjection;

namespace FlowSynx.Net;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpRequestService(this IServiceCollection services)
    {
        services.AddTransient<IHttpRequestService, HttpRequestService>();
        return services;
    }
}