namespace FlowSynx.Connectors.Storage.Options;

public class DeleteOptions: ICloneable
{
    public bool? Purge { get; set; } = false;

    public object Clone()
    {
        var clone = (DeleteOptions)MemberwiseClone();
        return clone;
    }
}