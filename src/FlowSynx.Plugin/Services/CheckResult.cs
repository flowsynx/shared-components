namespace FlowSynx.Plugin.Services;

public class CheckResult
{
    public CheckResult(object entity, CheckState state)
    {
        Entity = entity;
        State = state;
    }

    public object Entity { get; set; }
    public CheckState State { get; set; }
}