namespace FlowSynx.Connectors.Storage.Options;

public class ListOptions: ICloneable
{
    public string? Fields { get; set; }
    public string? Filters { get; set; }
    public bool CaseSensitive { get; set; } = false;
    public bool Recurse { get; set; } = false;
    public string? Sorts { get; set; }
    public string? Paging { get; set; }
    public bool? IncludeMetadata { get; set; } = false;

    public object Clone()
    {
        var clone = (ListOptions)MemberwiseClone();
        return clone;
    }
}