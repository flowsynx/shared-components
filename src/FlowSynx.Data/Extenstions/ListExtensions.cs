using System.Data;
using System.Reflection;

namespace FlowSynx.Data.Extensions;

public static class ListExtensions
{
    public static DataTable ToDataTable<T>(this IEnumerable<T> items)
    {
        var data = items.ToList();
        return data.ToDataTable();
    }

    public static DataTable ToDataTable<T>(this List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        var Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            var propType = prop.PropertyType;
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propType = Nullable.GetUnderlyingType(prop.PropertyType);
            }

            dataTable.Columns.Add(prop.Name, propType);
        }

        foreach (T item in items)
        {
            var values = new object?[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                values[i] = Props[i].GetValue(item);
            }
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }
}