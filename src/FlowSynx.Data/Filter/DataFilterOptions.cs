namespace FlowSynx.Data.Filter;

public class DataFilterOptions
{
    public string[]? Fields { get; set; }
    public string? FilterExpression { get; set; }
    public string? SortExpression { get; set; }
    public string? Limit { get; set; }
    public bool? CaseSensetive { get; set; } = false;

}