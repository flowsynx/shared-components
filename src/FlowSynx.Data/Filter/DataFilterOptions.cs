namespace FlowSynx.Data.Filter;

public class DataFilterOptions
{
    public string[]? Fields { get; set; } = Array.Empty<string>();
    public string? FilterExpression { get; set; } = string.Empty;
    public Sort[]? Sort { get; set; } = Array.Empty<Sort>();
    public string? Limit { get; set; } = string.Empty;
    public bool? CaseSensitive { get; set; } = false;
}