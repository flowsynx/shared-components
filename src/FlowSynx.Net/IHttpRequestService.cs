namespace FlowSynx.Net;

public interface IHttpRequestService
{
    Task<HttpResult<TResult?>> SendRequestAsync<TResult>(Request request, CancellationToken cancellationToken);
    Task<HttpResult<TResult?>> SendRequestAsync<TRequest, TResult>(Request<TRequest> request, CancellationToken cancellationToken);
    Task<HttpResult<Stream>> SendRequestAsync(Request request, CancellationToken cancellationToken);
    Task<HttpResult<Stream>> SendRequestAsync<TRequest>(Request<TRequest> request, CancellationToken cancellationToken);
}