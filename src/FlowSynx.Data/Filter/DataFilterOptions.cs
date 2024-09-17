namespace FlowSynx.Data.Filter;

public class DataFilterOptions
{
    public string[]? Fields { get; set; } = Array.Empty<string>();
    public string? FilterExpression { get; set; } = string.Empty;
    public string? SortExpression { get; set; } = string.Empty;
    public string? Limit { get; set; } = string.Empty;
    public bool? CaseSensitive { get; set; } = false;

}