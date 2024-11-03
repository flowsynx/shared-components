namespace FlowSynx.Connectors.Storage.Options;

public class PathOptions: ICloneable
{
    public string Path { get; set; } = string.Empty;

    public object Clone()
    {
        var clone = (PathOptions)MemberwiseClone();
        return clone;
    }
}