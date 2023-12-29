using System.Net.Http.Headers;
using System.Text;

namespace FlowSynx.Net;

internal static class HttpRequestExtensions
{
    internal static void AddMediaTypeHeader(this HttpRequestHeaders requestHeader, string mediaType)
    {
        requestHeader.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
    }

    public static void AddHeaders(this HttpRequestHeaders requestHeader, IDictionary<string, string> headers)
    {
        foreach (var header in headers)
            requestHeader.Add(header.Key, header.Value);
    }

    internal static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string uri, 
        IDictionary<string, string> headers, string mediaType, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, new Uri(uri));
        request.Headers.AddMediaTypeHeader(mediaType);
        request.Headers.AddHeaders(headers);
        return await httpClient.SendAsync(request, cancellationToken);
    }

    internal static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string uri,
        IDictionary<string, string> headers, string mediaType, string content,
        CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, new Uri(uri));
        request.Headers.AddMediaTypeHeader(mediaType);
        request.Headers.AddHeaders(headers);
        request.Content = new StringContent(content, Encoding.UTF8, mediaType);
        return await httpClient.SendAsync(request, cancellationToken);
    }
}
