namespace FlowSynx.Connectors.Storage.Options;

public class ListOptions: ICloneable
{
    public string? Fields { get; set; }
    public string? Filter { get; set; }
    public string? Sort { get; set; }
    public string? Paging { get; set; }
    public bool Recurse { get; set; } = false;
    public bool CaseSensitive { get; set; } = false;
    public bool? IncludeMetadata { get; set; } = false;

    public object Clone()
    {
        var clone = (ListOptions)MemberwiseClone();
        return clone;
    }
}