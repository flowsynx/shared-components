namespace FlowSynx.Plugin.Abstractions;

public class TransferData
{
    public required PluginNamespace PluginNamespace { get; set; }
    public required string PluginType { get; set; }
    public required TransferState State { get; set; }
    public string? ContentType { get; set; }
    public string? Content { get; set; }
    public required IEnumerable<string> Columns { get; set; }
    public required IEnumerable<TransferDataRow> Rows { get; set; }
}

public class TransferDataRow
{
    public required string Key { get; set; }
    public string? ContentType { get; set; }
    public string? Content { get; set; }
    public object?[]? Items { get; set; }
}