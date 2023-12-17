namespace FlowSynx.Abstractions;

public interface ISystemClock
{
    DateTime NowUtc { get; }
}