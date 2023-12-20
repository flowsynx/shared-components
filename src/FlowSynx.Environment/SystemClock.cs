namespace FlowSynx.Environment;

public class SystemClock : ISystemClock
{
    public DateTime NowUtc => DateTime.UtcNow;
}