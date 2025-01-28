using System.Data;

namespace FlowSynx.Data.Extensions;

public static class DataTableExtensions
{
    public static List<object> DataTableToList(this DataTable dataTable)
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

    public static List<object> DataTableToList(this InterchangeData dataTable)
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

    public static InterchangeData CopyToInterchangeData(this IEnumerable<InterchangeRow> rows, InterchangeData dataTable)
    {
        InterchangeData interchangeData = new InterchangeData(dataTable.TableName);
        interchangeData.Metadata = dataTable.Metadata;

        if (rows == null || !rows.Any())
            return interchangeData;

        var columns = rows.ElementAt(0).Table.Columns;
        foreach (DataColumn column in columns)
        {
            interchangeData.Columns.Add(column.ColumnName, column.DataType);
        }

        foreach (InterchangeRow row in rows)
        {
            interchangeData.ImportRow(row);
        }

        return interchangeData;
    }
}