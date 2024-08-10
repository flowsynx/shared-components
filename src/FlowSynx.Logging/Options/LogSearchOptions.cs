namespace FlowSynx.Logging.Options;

public class LogSearchOptions
{
    public string? Include { get; set; }
    public string? Exclude { get; set; }
    public string? MinimumAge { get; set; }
    public string? MaximumAge { get; set; }
    public string? Level { get; set; }
    public bool CaseSensitive { get; set; } = false;
}