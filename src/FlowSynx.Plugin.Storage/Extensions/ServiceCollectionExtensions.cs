using System.Data;
using System.Reflection;

namespace FlowSynx.Plugin.Storage.Extensions;

public static class DataTableExtensions
{
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

    public static List<object> CreateListFromTable(this DataTable dataTable)
    {
        var result = new List<object>();
        var colCount = dataTable.Columns.Count;
        foreach (DataRow dr in dataTable.Rows)
        {
            dynamic objExpando = new System.Dynamic.ExpandoObject();
            var obj = objExpando as IDictionary<string, object>;

            for (var i = 0; i < colCount; i++)
            {
                var key = dr.Table.Columns[i].ColumnName.ToString();
                var val = dr[key];

                if (obj != null)
                    obj[key] = val;
            }

            if (obj != null)
                result.Add(obj);
        }

        return result;
    }
}