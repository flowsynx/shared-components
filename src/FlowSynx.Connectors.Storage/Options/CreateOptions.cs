﻿namespace FlowSynx.Connectors.Storage.Options;

public class CreateOptions
{
    public string Path { get; set; } = string.Empty;
    public bool? Hidden { get; set; } = false;
}