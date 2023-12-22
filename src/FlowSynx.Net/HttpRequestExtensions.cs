using FlowSynx.IO.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return response;
    }

    internal static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string uri,
        IDictionary<string, string> headers, string mediaType, string content,
        CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, new Uri(uri));
        request.Headers.AddMediaTypeHeader(mediaType);
        request.Headers.AddHeaders(headers);
        request.Content = new StringContent(content, Encoding.UTF8, mediaType);
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return response;
    }
}
