namespace FlowSynx.Plugin.Storage.Check;

public enum StorageCheckState
{
    Error = 0,
    Match = 1,
    Different = 2,
    MissedOnDestination = 4,
    MissedOnSource = 8,
}