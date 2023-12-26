using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging;

public class LogMessage
{
    public string UserName { get; } = Environment.UserName;
    public string Machine { get; }= System.Net.Dns.GetHostName();
    public DateTime TimeStamp { get; } = DateTime.UtcNow;
    public required string Category { get; set; }
    public required string Message { get; set; }
    public required LogLevel Level { get; set; }
    public EventId EventId { get; set; }
}