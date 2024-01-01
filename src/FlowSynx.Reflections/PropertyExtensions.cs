using System.Reflection;

namespace FlowSynx.Reflections;

public static class PropertyExtensions
{
    public static PropertyInfo? Property(this Type type, string propertyName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
    {
        return !type.IsInterface ?
            type.GetProperty(propertyName, bindingAttr) :
            type.GetInterfaces().Union(new Type[] { type }).Select(i => i.GetProperty(propertyName, bindingAttr)).Distinct().Single(propertyInfo => propertyInfo != null);
    }

    public static List<PropertyInfo> Properties(this Type type, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
    {
        return !type.IsInterface ?
                type.GetProperties(bindingAttr).ToList() :
                type.GetInterfaces().Union(new[] { type }).SelectMany(i => i.GetProperties(bindingAttr)).Distinct().ToList();
    }

    public static object? GetPropertyValue(this object source, string propertyName)
    {
        var property = source.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
        return property?.GetValue(source, null);
    }

    public static object? GetPropertyValue(this PropertyInfo property, object? instance)
    {
        return property?.GetValue(instance, null);
    }

    public static Type GetPropertyType(this PropertyInfo property)
    {
        return property.PropertyType;
    }
    
    public static bool IsGenericType(this PropertyInfo property)
    {
        return property.GetPropertyType().IsGenericType;
    }
    
    public static bool IsDictionaryType(this PropertyInfo property)
    {
        return property.GetPropertyType().IsGenericType && property.GetPropertyType().GetGenericTypeDefinition() == typeof(Dictionary<,>);
    }
}