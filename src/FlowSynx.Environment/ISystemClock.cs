namespace FlowSynx.Environment;

public interface ISystemClock
{
    DateTime NowUtc { get; }
}