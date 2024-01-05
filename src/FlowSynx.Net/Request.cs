using System.Net.Mime;

namespace FlowSynx.Net;

public class Request
{
    public required HttpMethod HttpMethod { get; set; } = HttpMethod.Get;
    public required string Uri { get; set; }
    public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string MediaType { get; set; } = MediaTypeNames.Application.Json;
}

public class Request<T>: Request
{
    public T? Content { get; set; }
}