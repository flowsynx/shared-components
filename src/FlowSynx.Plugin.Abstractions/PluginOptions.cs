﻿namespace FlowSynx.Plugin.Abstractions;

public class PluginOptions : Dictionary<string, object?>
{
    public void ChangeProperty(string propertyName, object propertyValue)
    {
        if (this.ContainsKey(propertyName))
        {
            this[propertyName] = propertyValue;
        }
    }
}