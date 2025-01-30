using System.Globalization;
using System.Reflection;
using System.Text.Json;
using FlowSynx.Reflections;
using Newtonsoft.Json;

namespace FlowSynx.Connectors.Abstractions.Extensions;

public static class ConnectorOptionsExtensions
{
    public static ConnectorOptions ToConnectorOptions(this Dictionary<string, object?>? source)
    {
        var options = new ConnectorOptions();
        if (source is null)
            return options;

        foreach (var item in source)
        {
            options.Add(item.Key, item.Value);
        }
        return options;
    }

    public static T ToObject<T>(this ConnectorOptions? source) where T : class, new()
    {
        var newInstance = new T();
        if (source is null) return newInstance;

        var someObjectType = newInstance.GetType();
        foreach (var item in source)
        {
            var property = someObjectType.Property(item.Key, BindingFlags.Public 
                                                             | BindingFlags.Instance 
                                                             | BindingFlags.IgnoreCase);

            newInstance.SetPropertyValue(property, item.Value);
        }

        return newInstance;
    }

    public static void SetPropertyValue<T>(this T instance, PropertyInfo? property, object? value)
    {
        if (property != null && property.CanWrite)
        {
            try
            {
                // Handle null values explicitly
                if (value == null || value.ToString() == "null")
                {
                    // If the property is nullable, set to null
                    if (Nullable.GetUnderlyingType(property.PropertyType) != null || !property.PropertyType.IsValueType)
                    {
                        property.SetValue(instance, null);
                    }
                    return;
                }

                object? convertedValue;

                if (property.PropertyType == typeof(string))
                {
                    convertedValue = string.IsNullOrWhiteSpace(value.ToString()) ? null : value.ToString();
                }
                else if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(decimal))
                {
                    convertedValue = Convert.ChangeType(value, property.PropertyType);
                }
                else if (property.PropertyType.IsGenericType &&
                         (property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                          || property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    // Handle List<T> using System.Text.Json
                    convertedValue = System.Text.Json.JsonSerializer.Deserialize(value.ToString(), property.PropertyType);
                }
                else if (property.PropertyType.IsGenericType &&
                         property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    // Handle Dictionary<string, object>
                    if (property.PropertyType == typeof(Dictionary<string, object>))
                    {
                        convertedValue = JsonConvert.DeserializeObject<Dictionary<string, object>>(value.ToString());
                    }
                    else
                    {
                        // Handle Dictionary with other types
                        convertedValue = System.Text.Json.JsonSerializer.Deserialize(value.ToString(), property.PropertyType);
                    }
                }
                else if (value.ToString().IsJson())
                {
                    // Try Newtonsoft.Json for complex types
                    try
                    {
                        convertedValue = JsonConvert.DeserializeObject(value.ToString(), property.PropertyType);
                    }
                    catch
                    {
                        // If Newtonsoft fails, fallback to System.Text.Json
                        convertedValue = System.Text.Json.JsonSerializer.Deserialize(value.ToString(), property.PropertyType);
                    }
                }
                else
                {
                    convertedValue = Convert.ChangeType(value, property.PropertyType);
                }

                // Check for null values (only assign if the value is valid, otherwise use default)
                if (convertedValue == null && property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) == null)
                {
                    convertedValue = property.PropertyType.GetDefaultValue();
                }

                property.SetValue(instance, convertedValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting property {property.Name}: {ex.Message}");
            }
        }
    }

    private static bool IsJson(this string? input)
    {
        input = input == null ? string.Empty : input.Trim();
        return (input.StartsWith("{") && input.EndsWith("}")) || // Object
               (input.StartsWith("[") && input.EndsWith("]"));  // Array
    }

    private static object? GetDefaultValue(this Type type)
    {
        // Return default value for a given type
        return type.IsValueType ? Activator.CreateInstance(type) : null;
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