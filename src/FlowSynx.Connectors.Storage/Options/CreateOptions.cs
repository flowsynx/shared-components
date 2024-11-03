namespace FlowSynx.Connectors.Storage.Options;

public class CreateOptions: ICloneable
{
    public bool? Hidden { get; set; } = false;

    public object Clone()
    {
        var clone = (CreateOptions)MemberwiseClone();
        return clone;
    }
}