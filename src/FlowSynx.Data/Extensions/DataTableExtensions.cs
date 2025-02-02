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

    public static InterchangeData CopyToInterchangeData(this List<InterchangeRow> rowList, InterchangeMetadata metadata)
    {
        if (rowList == null || rowList.Count == 0) return new InterchangeData();

        InterchangeData newTable = new InterchangeData();
        newTable = (InterchangeData)rowList[0].Table.Clone(); // Copy schema
        newTable.Metadata = metadata;

        foreach (InterchangeRow row in rowList)
        {
            InterchangeRow newRow = newTable.NewRow();

            // Copy column values
            newRow.ItemArray = row.ItemArray;

            // Copy metadata
            newRow.Metadata = row.Metadata;

            newTable.Rows.Add(newRow);
        }

        return newTable;
    }

    public static InterchangeData CopyToInterchangeData2(this IEnumerable<InterchangeRow> rows, InterchangeData dataTable)
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

            interchangeData.ImportMetadata(row);
        }

        //for (int i = 0; i < rows.Count(); i++)
        //{
        //    var row = rows.ElementAt(i);
        //    var newRow = interchangeData.NewRow();

        //    // Copy values
        //    for (int j = 0; i < dataTable.Columns.Count; i++)
        //    {
        //        newRow[j] = row[j];
        //    }

        //    // Copy metadata
        //    newRow.RowError = row.RowError;
        //    newRow.ItemArray = row.ItemArray.Clone() as object[]; // Copies values deeply
        //    newRow.AcceptChanges(); // Maintain state
        //    newRow.ImportMetadata(row);

        //    interchangeData.Rows.Add(newRow);
        //}

        return interchangeData;
    }
}