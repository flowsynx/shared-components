namespace FlowSynx.Net;

public interface IHttpRequestService
{
    Task<TResult?> SendRequestAsync<TResult>(Request request, CancellationToken cancellationToken);
    Task<TResult?> SendRequestAsync<TRequest, TResult>(Request<TRequest> request, CancellationToken cancellationToken);
    Task<Stream> SendRequestAsync<TRequest>(Request<TRequest> request, CancellationToken cancellationToken);
}