using System.Reflection;

namespace FlowSynx.Reflections;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetInterfaces(this Type type)
    {
        return type.GetInterfaces().AsEnumerable();
    }

    public static bool IsInterface(this Type type)
    {
        return type.IsInterface;
    }

    public static bool IsClass(this Type type)
    {
        return type.IsClass;
    }

    public static bool IsGenericType(this Type type)
    {
        return type.IsGenericType;
    }

    public static bool IsDictionaryType(this Type type)
    {
        return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
    }

    public static bool IsAssignable(this Type concretion, Type abstraction)
    {
        return abstraction.IsAssignableFrom(concretion);
    }

    public static T DictionaryToObject<T>(this Dictionary<string, string?>? source,
        BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase) 
        where T : class, new()
    {
        var newInstance = new T();
        if (source is null) return newInstance;

        var someObjectType = newInstance.GetType();
        foreach (var item in source)
        {
            var property = someObjectType.Property(item.Key, bindingAttr);
            if (property != null)
                property.SetValue(newInstance, item.Value, null);
        }

        return newInstance;
    }
}