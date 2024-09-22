namespace FlowSynx.Plugin.Abstractions;

public class TransmissionData
{
    public required PluginNamespace Namespace { get; set; }
    public required string Type { get; set; }
    public required IEnumerable<string> Columns { get; set; }
    public required IEnumerable<TransmissionDataRow> Rows { get; set; }
}

public class TransmissionDataRow
{
    public string? Key { get; set; }
    public string? ContentType { get; set; }
    public Stream? Content { get; set; }
    public object[]? Data { get; set; }
}