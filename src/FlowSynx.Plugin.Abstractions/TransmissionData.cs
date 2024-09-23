﻿namespace FlowSynx.Plugin.Abstractions;

public class TransmissionData
{
    public required PluginNamespace PluginNamespace { get; set; }
    public required string PluginType { get; set; }
    public string? ContentType { get; set; }
    public string? Content { get; set; }
    public required IEnumerable<string> Columns { get; set; }
    public required IEnumerable<TransmissionDataRow> Rows { get; set; }
}

public class TransmissionDataRow
{
    public required string Key { get; set; }
    public string? ContentType { get; set; }
    public string? Content { get; set; }
    public object?[]? Items { get; set; }
}