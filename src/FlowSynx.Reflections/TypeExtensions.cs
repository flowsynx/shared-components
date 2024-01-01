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
        return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
    }

    public static bool IsAssignable(this Type concretion, Type abstraction)
    {
        return abstraction.IsAssignableFrom(concretion);
    }
}