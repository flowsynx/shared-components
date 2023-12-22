using FlowSynx.IO.Serialization;
using FlowSynx.Net.Exceptions;
using EnsureThat;

namespace FlowSynx.Net;

public class HttpRequestService : IHttpRequestService
{
    private readonly HttpClient _httpClient;
    private readonly ISerializer _serializer;
    private readonly IDeserializer _deserializer;
    private const string MediaType = "application/json";

    public HttpRequestService(HttpClient httpClient, ISerializer serializer, IDeserializer deserializer)
    {
        EnsureArg.IsNotNull(httpClient, nameof(httpClient));
        EnsureArg.IsNotNull(serializer, nameof(serializer));
        EnsureArg.IsNotNull(deserializer, nameof(deserializer));
        _httpClient = httpClient;
        _serializer = serializer;
        _deserializer = deserializer;
    }

    public HttpClient GetClient()
    {
        return _httpClient;
    }

    public async Task<TResult> GetAsync<TResult>(string uri, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>();
        return await GetAsync<TResult>(uri, headers, cancellationToken);
    }

    public async Task<TResult> GetAsync<TResult>(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.SendAsync(HttpMethod.Get, uri, headers, MediaType, cancellationToken);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
            return _deserializer.Deserialize<TResult>(responseJson);
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
        catch (Exception ex)
        {
            throw new RequestServiceException(string.Format(Resources.RequestServiceException, ex.Message));
        }
    }

    public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest content, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>();
        return await PostAsync<TRequest, TResult>(uri, content, headers, cancellationToken);
    }

    public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest content, IDictionary<string, string> headers, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.SendAsync(HttpMethod.Post, uri, headers, MediaType, _serializer.Serialize(content), cancellationToken);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
            return _deserializer.Deserialize<TResult>(responseJson);
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
        catch (Exception ex)
        {
            throw new RequestServiceException(string.Format(Resources.RequestServiceException, ex.Message));
        }
    }

    public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest content, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>();
        return await PutAsync<TRequest, TResult>(uri, content, headers, cancellationToken);
    }

    public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest content, IDictionary<string, string> headers, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.SendAsync(HttpMethod.Put, uri, headers, MediaType, _serializer.Serialize(content), cancellationToken);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
            return _deserializer.Deserialize<TResult>(responseJson);
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
        catch (Exception ex)
        {
            throw new RequestServiceException(string.Format(Resources.RequestServiceException, ex.Message));
        }
    }

    public async Task<TResult> DeleteAsync<TRequest, TResult>(string uri, TRequest content, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>();
        return await DeleteAsync<TRequest, TResult>(uri, content, headers, cancellationToken);
    }

    public async Task<TResult> DeleteAsync<TRequest, TResult>(string uri, TRequest content, IDictionary<string, string> headers, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.SendAsync(HttpMethod.Delete, uri, headers, MediaType, _serializer.Serialize(content), cancellationToken);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
            return _deserializer.Deserialize<TResult>(responseJson);
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
        catch (Exception ex)
        {
            throw new RequestServiceException(string.Format(Resources.RequestServiceException, ex.Message));
        }
    }
}