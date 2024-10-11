using System.Reflection;
using FlowSynx.Reflections;

namespace FlowSynx.Connectors.Abstractions.Extensions;

public static class SpecificationsExtensions
{
    public static Specifications ToSpecifications(this Dictionary<string, string?>? source)
    {
        var specifications = new Specifications();
        if (source is null) 
            return specifications;

        foreach (var item in source)
        {
            specifications.Add(item.Key, item.Value);
        }
        return specifications;
    }

    public static T ToObject<T>(this Specifications? source) where T : class, new()
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
                property.SetValue(newInstance, item.Value, null);
        }

        return newInstance;
    }
}