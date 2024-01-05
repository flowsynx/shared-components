namespace FlowSynx.Net;

public static class HttpRequestExtensions
{
    #region Get
    public static async Task<TResult?> GetRequestAsync<TResult>(this IHttpRequestService httpRequestService, string uri, CancellationToken cancellationToken = default)
    {
        var message = new Request
        {
            Uri = uri,
            HttpMethod = HttpMethod.Get
        };

        return await httpRequestService.SendRequestAsync<TResult>(message, cancellationToken);
    }

    public static async Task<TResult?> GetRequestAsync<TResult>(this IHttpRequestService httpRequestService, string uri, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        var message = new Request
        {
            Uri = uri,
            Headers = headers,
            HttpMethod = HttpMethod.Get
        };

        return await httpRequestService.SendRequestAsync<TResult>(message, cancellationToken);
    }

    public static async Task<TResult?> GetRequestAsync<TRequest, TResult>(this IHttpRequestService httpRequestService, string uri, TRequest request, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            HttpMethod = HttpMethod.Post,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest, TResult>(message, cancellationToken);
    }

    public static async Task<TResult?> GetRequestAsync<TRequest, TResult>(this IHttpRequestService httpRequestService, string uri, TRequest request, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            Headers = headers,
            HttpMethod = HttpMethod.Get,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TResult>(message, cancellationToken);
    }

    public static async Task<Stream> GetRequestAsync(this IHttpRequestService httpRequestService, string uri, CancellationToken cancellationToken = default)
    {
        var message = new Request
        {
            Uri = uri,
            HttpMethod = HttpMethod.Get
        };

        return await httpRequestService.SendRequestAsync(message, cancellationToken);
    }

    public static async Task<Stream> GetRequestAsync(this IHttpRequestService httpRequestService, string uri, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        var message = new Request
        {
            Uri = uri,
            Headers = headers,
            HttpMethod = HttpMethod.Get
        };

        return await httpRequestService.SendRequestAsync(message, cancellationToken);
    }

    public static async Task<Stream> GetRequestAsync<TRequest>(this IHttpRequestService httpRequestService, string uri, TRequest request, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            HttpMethod = HttpMethod.Get,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest>(message, cancellationToken);
    }

    public static async Task<Stream> GetRequestAsync<TRequest>(this IHttpRequestService httpRequestService, string uri, TRequest request, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            Headers = headers,
            HttpMethod = HttpMethod.Get,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest>(message, cancellationToken);
    }
    #endregion

    #region Post
    public static async Task<TResult?> PostRequestAsync<TRequest, TResult>(this IHttpRequestService httpRequestService, string uri, TRequest request, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            HttpMethod = HttpMethod.Post,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest, TResult>(message, cancellationToken);
    }

    public static async Task<TResult?> PostRequestAsync<TRequest, TResult>(this IHttpRequestService httpRequestService, string uri, TRequest request, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            Headers = headers,
            HttpMethod = HttpMethod.Post,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest, TResult>(message, cancellationToken);
    }

    public static async Task<Stream> PostRequestAsync<TRequest>(this IHttpRequestService httpRequestService, string uri, TRequest request, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            HttpMethod = HttpMethod.Post,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest>(message, cancellationToken);
    }

    public static async Task<Stream> PostRequestAsync<TRequest>(this IHttpRequestService httpRequestService, string uri, TRequest request, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            Headers = headers,
            HttpMethod = HttpMethod.Post,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest>(message, cancellationToken);
    }
    #endregion

    #region Put
    public static async Task<TResult?> PutRequestAsync<TRequest, TResult>(this IHttpRequestService httpRequestService, string uri, TRequest request, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            HttpMethod = HttpMethod.Put,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest, TResult>(message, cancellationToken);
    }

    public static async Task<TResult?> PutRequestAsync<TRequest, TResult>(this IHttpRequestService httpRequestService, string uri, TRequest request, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            Headers = headers,
            HttpMethod = HttpMethod.Put,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest, TResult>(message, cancellationToken);
    }
    #endregion

    #region Delete
    public static async Task<TResult?> DeleteRequestAsync<TRequest, TResult>(this IHttpRequestService httpRequestService, string uri, TRequest request, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            HttpMethod = HttpMethod.Delete,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest, TResult>(message, cancellationToken);
    }

    public static async Task<TResult?> DeleteRequestAsync<TRequest, TResult>(this IHttpRequestService httpRequestService, string uri, TRequest request, IDictionary<string, string> headers, CancellationToken cancellationToken = default)
    {
        var message = new Request<TRequest>
        {
            Uri = uri,
            Headers = headers,
            HttpMethod = HttpMethod.Delete,
            Content = request
        };

        return await httpRequestService.SendRequestAsync<TRequest, TResult>(message, cancellationToken);
    }
    #endregion
}
