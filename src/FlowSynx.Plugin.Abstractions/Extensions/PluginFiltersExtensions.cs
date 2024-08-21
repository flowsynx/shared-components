using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using FlowSynx.Reflections;

namespace FlowSynx.Plugin.Abstractions.Extensions;

public static class PluginFiltersExtensions
{
    public static PluginFilters ToPluginFilters(this Dictionary<string, object?>? source)
    {
        var pluginFilters = new PluginFilters();
        if (source is null)
            return pluginFilters;

        foreach (var item in source)
        {
            pluginFilters.Add(item.Key, item.Value);
        }
        return pluginFilters;
    }

    public static T ToObject<T>(this PluginFilters? source) where T : class, new()
    {
        var newInstance = new T();
        if (source is null) return newInstance;

        var someObjectType = newInstance.GetType();
        foreach (var item in source)
        {
            var property = someObjectType.Property(item.Key, BindingFlags.Public 
                                                             | BindingFlags.Instance 
                                                             | BindingFlags.IgnoreCase);
            if (property != null)
                property.SetValue(newInstance, item.Value.GetObjectValue(), null);
        }

        return newInstance;
    }

    public static object? GetObjectValue(this object? obj)
    {
        switch (obj)
        {
            case null:
                return string.Empty;
            case string:
                return string.IsNullOrEmpty(obj.ToString()) ? string.Empty : obj.ToString();
            case JsonElement jsonElement:
            {
                var typeOfObject = jsonElement.ValueKind;
                var rawText = jsonElement.GetRawText();

                return typeOfObject switch
                {
                    JsonValueKind.Number => float.Parse(rawText, CultureInfo.InvariantCulture),
                    JsonValueKind.String => obj.ToString(),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.Null => null,
                    JsonValueKind.Undefined => null,
                    JsonValueKind.Object => rawText,
                    JsonValueKind.Array => rawText,
                    _ => rawText
                };
            }
            default:
                return obj;
        }
    }
}