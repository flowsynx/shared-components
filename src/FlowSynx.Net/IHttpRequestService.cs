namespace FlowSynx.Net;

public interface IHttpRequestService
{
    HttpClient GetClient();
    Task<TResult> GetAsync<TResult>(string uri, CancellationToken cancellationToken);
    Task<TResult> GetAsync<TResult>(string uri, IDictionary<string, string> headers, CancellationToken cancellationToken);
    Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest content, CancellationToken cancellationToken);
    Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest content, IDictionary<string, string> headers, CancellationToken cancellationToken);
    Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest content, CancellationToken cancellationToken);
    Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest content, IDictionary<string, string> headers, CancellationToken cancellationToken);
    Task<TResult> DeleteAsync<TRequest, TResult>(string uri, TRequest content, CancellationToken cancellationToken);
    Task<TResult> DeleteAsync<TRequest, TResult>(string uri, TRequest content, IDictionary<string, string> headers, CancellationToken cancellationToken);
}