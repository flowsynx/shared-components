using System.Data;

namespace FlowSynx.Data.Extensions;

public static class DataViewExtensions
{
    public static InterchangeData ConvertDataViewToCustomTable(DataView dataView, string tableName)
    {
        // Initialize a new instance of the custom DataTable
        InterchangeData customTable = new InterchangeData(tableName);

        // Copy columns
        foreach (DataColumn column in dataView.Table.Columns)
        {
            DataColumn newColumn = new DataColumn(column.ColumnName, column.DataType)
            {
                ReadOnly = column.ReadOnly,
                Unique = column.Unique,
                AllowDBNull = column.AllowDBNull
            };
            customTable.Columns.Add(newColumn);
        }

        // Copy rows
        foreach (DataRowView rowView in dataView)
        {
            DataRow newRow = customTable.NewRow();
            foreach (DataColumn column in customTable.Columns)
            {
                newRow[column.ColumnName] = rowView[column.ColumnName];
            }
            customTable.Rows.Add(newRow);
        }

        return customTable;
    }

}