namespace FlowSynx.Plugin.Abstractions.Filter;

public class DataFilterOptions
{
    public string? FilterExpression { get; set; }
    public string? SortExpression { get; set; }
    public string? Limit { get; set; }
    public bool? CaseSensetive { get; set; } = false;

}