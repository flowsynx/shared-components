namespace FlowSynx.Connectors.Storage.Options;

public class ReadOptions: ICloneable
{
    public bool? Hashing { get; set; } = false;

    public object Clone()
    {
        var clone = (ReadOptions)MemberwiseClone();
        return clone;
    }
}