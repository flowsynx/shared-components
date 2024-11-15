using System.Data;

namespace FlowSynx.Data.DataTableQuery.Extensions;

public static class DataTableExtensions
{
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