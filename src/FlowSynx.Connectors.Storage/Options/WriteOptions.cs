using FlowSynx.Connectors.Abstractions;

namespace FlowSynx.Connectors.Storage.Options;

public class WriteOptions: ICloneable
{
    public object? Data { get; set; }
    public bool? Overwrite { get; set; } = false;

    public object Clone()
    {
        var clone = (WriteOptions)MemberwiseClone();
        return clone;
    }
}