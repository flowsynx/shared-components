namespace FlowSynx.Data.Queries;

public class SelectDataOption
{
    public FieldsList? Fields { get; set; } = new();
    public FiltersList? Filters { get; set; } = new();
    public SortsList? Sorts { get; set; } = new();
    public Paging? Paging { get; set; } = new();
    public bool? CaseSensitive { get; set; } = false;
}
