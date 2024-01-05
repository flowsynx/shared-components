using FlowSynx.IO.Serialization;
using FlowSynx.Net.Exceptions;
using EnsureThat;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace FlowSynx.Net;

public class HttpRequestService : IHttpRequestService
{
    private readonly HttpClient _httpClient;
    private readonly ISerializer _serializer;
    private readonly IDeserializer _deserializer;

    public HttpRequestService(HttpClient httpClient, ISerializer serializer, IDeserializer deserializer)
    {
        EnsureArg.IsNotNull(httpClient, nameof(httpClient));
        EnsureArg.IsNotNull(serializer, nameof(serializer));
        EnsureArg.IsNotNull(deserializer, nameof(deserializer));
        _httpClient = httpClient;
        _serializer = serializer;
        _deserializer = deserializer;
    }

    public async Task<TResult?> SendRequestAsync<TResult>(Request request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await CreateHttpRequestAsync(request, cancellationToken).ConfigureAwait(false);
            var responseContent = response.Content;
            var responseString = await responseContent.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return _deserializer.Deserialize<TResult>(responseString);
        }
        catch (HttpRequestException)
        {
            throw new RequestServiceException(Resources.RequestServiceHttpRequestExceptionMessage);
        }
        catch (TimeoutException)
        {
            throw new RequestServiceException(Resources.RequestServiceTimeoutException);
        }
        catch (OperationCanceledException)
        {
            throw new RequestServiceException(Resources.RequestServiceOperationCanceledException);
        }
        catch (JsonException ex)
        {
            throw new RequestServiceException(Resources.PayloadCouldNotBeDeserialized, ex);
        }
        catch (Exception ex)
        {
            throw new RequestServiceException(string.Format(Resources.RequestServiceException, ex.Message));
        }
    }

    public async Task<TResult?> SendRequestAsync<TRequest, TResult>(Request<TRequest> request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await CreateHttpRequestAsync(request, cancellationToken).ConfigureAwait(false);
            var responseContent = response.Content;
            var responseString = await responseContent.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return _deserializer.Deserialize<TResult>(responseString);
        }
        catch (HttpRequestException)
        {
            throw new RequestServiceException(Resources.RequestServiceHttpRequestExceptionMessage);
        }
        catch (TimeoutException)
        {
            throw new RequestServiceException(Resources.RequestServiceTimeoutException);
        }
        catch (OperationCanceledException)
        {
            throw new RequestServiceException(Resources.RequestServiceOperationCanceledException);
        }
        catch (JsonException ex)
        {
            throw new RequestServiceException(Resources.PayloadCouldNotBeDeserialized, ex);
        }
        catch (Exception ex)
        {
            throw new RequestServiceException(string.Format(Resources.RequestServiceException, ex.Message));
        }
    }

    public async Task<Stream> SendRequestAsync<TRequest>(Request<TRequest> request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await CreateHttpRequestAsync(request, cancellationToken).ConfigureAwait(false);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
        catch (HttpRequestException)
        {
            throw new RequestServiceException(Resources.RequestServiceHttpRequestExceptionMessage);
        }
        catch (TimeoutException)
        {
            throw new RequestServiceException(Resources.RequestServiceTimeoutException);
        }
        catch (OperationCanceledException)
        {
            throw new RequestServiceException(Resources.RequestServiceOperationCanceledException);
        }
        catch (JsonException ex)
        {
            throw new RequestServiceException(Resources.PayloadCouldNotBeDeserialized, ex);
        }
        catch (Exception ex)
        {
            throw new RequestServiceException(string.Format(Resources.RequestServiceException, ex.Message));
        }
    }

    #region private methods
    private async Task<HttpResponseMessage> CreateHttpRequestAsync<TRequest>(Request<TRequest> request, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage(request.HttpMethod, new Uri(request.Uri));

        if (!string.IsNullOrEmpty(request.MediaType))
            AddMediaTypeHeader(message.Headers, request.MediaType);

        if (request.Headers.Count > 0)
            AddHeaders(message.Headers, request.Headers);

        if (request.Content != null)
            message.Content = new StringContent(_serializer.Serialize(request.Content), Encoding.UTF8, request.MediaType);

        return await _httpClient.SendAsync(message, cancellationToken);
    }

    private async Task<HttpResponseMessage> CreateHttpRequestAsync(Request request, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage(request.HttpMethod, new Uri(request.Uri));

        if (!string.IsNullOrEmpty(request.MediaType))
            AddMediaTypeHeader(message.Headers, request.MediaType);

        if (request.Headers.Count > 0)
            AddHeaders(message.Headers, request.Headers);

        return await _httpClient.SendAsync(message, cancellationToken);
    }

    private void AddMediaTypeHeader(HttpRequestHeaders requestHeader, string mediaType)
    {
        requestHeader.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
    }

    private void AddHeaders(HttpRequestHeaders requestHeader, IDictionary<string, string> headers)
    {
        foreach (var header in headers)
            requestHeader.Add(header.Key, header.Value);
    }
    #endregion
}