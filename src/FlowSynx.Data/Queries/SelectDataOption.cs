namespace FlowSynx.Data.Queries;

public class SelectDataOption
{
    public FieldsList? Fields { get; set; } = new();
    public FilterList? Filter { get; set; } = new();
    public SortList? Sort { get; set; } = new();
    public Paging? Paging { get; set; } = new();
    public bool? CaseSensitive { get; set; } = false;
}
