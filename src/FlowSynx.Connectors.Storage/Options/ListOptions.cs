﻿namespace FlowSynx.Connectors.Storage.Options;

public class ListOptions
{
    public string Path { get; set; } = string.Empty;
    public string? Fields { get; set; }
    public string? Filter { get; set; }
    public bool CaseSensitive { get; set; } = false;
    public bool Recurse { get; set; } = false;
    public string? Sort { get; set; }
    public string? Limit { get; set; }
    public bool? IncludeMetadata { get; set; } = false;
}