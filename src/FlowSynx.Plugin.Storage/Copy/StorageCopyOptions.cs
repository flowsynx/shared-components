﻿namespace FlowSynx.Plugin.Storage.Copy;

public class StorageCopyOptions
{
    public bool? ClearDestinationPath { get; set; } = false;
    public bool? OverWriteData { get; set; } = false;

}