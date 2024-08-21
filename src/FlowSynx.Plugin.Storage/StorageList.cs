﻿namespace FlowSynx.Plugin.Storage;

internal class StorageList
{
    public string? Id { get; set; }
    public string? Kind { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Path { get; set; } = string.Empty;
    public string? Size { get; set; }
    public string? ContentType { get; set; }
    public DateTimeOffset? ModifiedTime { get; set; }
    public string? Md5 { get; set; } = string.Empty;
    public Dictionary<string, object>? Metadata { get; set; }
}