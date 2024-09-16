namespace FlowSynx.Logging;

internal class LogMessageResponse
{
    public string UserName { get; set; } = System.Environment.UserName;
    public string Machine { get; set; } = System.Net.Dns.GetHostName();
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    public string Message { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
}