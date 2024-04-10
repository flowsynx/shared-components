namespace FlowSynx.Net;

public class HttpResult<TResult>
{
    public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; } = new List<KeyValuePair<string, IEnumerable<string>>>();
    public required TResult Payload { get; set; }
}