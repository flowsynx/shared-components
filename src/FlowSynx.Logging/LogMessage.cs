using FlowSynx.Abstractions.Attributes;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Logging;

public class LogMessage
{
    [SortMember]
    public string UserName { get; } = System.Environment.UserName;

    [SortMember]
    public string Machine { get; }= System.Net.Dns.GetHostName();

    [SortMember]
    public DateTime TimeStamp { get; } = DateTime.UtcNow;

    public required string Category { get; set; }

    [SortMember]
    public required string Message { get; set; }

    [SortMember]
    public required LogLevel Level { get; set; }

    public EventId EventId { get; set; }
}