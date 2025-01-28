using System.Data;
using System.Reflection;

namespace FlowSynx.Data.Extensions;

public static class ListExtensions
{
    public static DataTable ListToDataTable<T>(this IEnumerable<T> items)
    {
        var data = items.ToList();
        return data.ListToDataTable();
    }

    public static DataTable ListToDataTable<T>(this List<T> items)
    {
        var dataTable = new DataTable(typeof(T).Name);
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            var propType = prop.PropertyType;

            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                propType = Nullable.GetUnderlyingType(prop.PropertyType);

            if (propType != null)
                dataTable.Columns.Add(prop.Name, propType);
        }

        foreach (var item in items)
        {
            var values = new object?[props.Length];
            for (var i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }

    public static InterchangeData ListToInterchangeData<T>(this IEnumerable<T> items)
    {
        var data = items.ToList();
        return data.ListToInterchangeData();
    }

    public static InterchangeData ListToInterchangeData<T>(this List<T> items)
    {
        var dataTable = new InterchangeData();
        dataTable.TableName = typeof(T).Name;
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            var propType = prop.PropertyType;

            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                propType = Nullable.GetUnderlyingType(prop.PropertyType);

            if (propType != null)
                dataTable.Columns.Add(prop.Name, propType);
        }

        foreach (var item in items)
        {
            var values = new object?[props.Length];
            for (var i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }
}