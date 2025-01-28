using FlowSynx.Data.Exceptions;
using System.Data;

namespace FlowSynx.Data;

public class InterchangeDataRowView
{
    private readonly InterchangeRow _dataRow;

    public InterchangeDataRowView(InterchangeRow dataRow)
    {
        _dataRow = dataRow;
    }

    public object this[int columnIndex] => _dataRow[columnIndex];
    public InterchangeRowMetadata Metadata => _dataRow.Metadata;
}


public class InterchangeDataView : DataView
{
    private readonly InterchangeMetadata _metadata;

    public InterchangeDataView(InterchangeData dataTable) : base(dataTable) 
    {
        _metadata = dataTable.Metadata;
    }

    public IEnumerable<InterchangeDataRowView> GetDataRowViews()
    {
        foreach (DataRowView rowView in this)
        {
            if (rowView.Row is InterchangeRow interchangeRow)
            {
                yield return new InterchangeDataRowView(interchangeRow);
            }
        }
    }

    public new InterchangeData ToTable()
    {
        return ToTable(null, Array.Empty<string>());
    }

    public new InterchangeData ToTable(string? tableName)
    {
        return ToTable(tableName, Array.Empty<string>());
    }

    public InterchangeData ToTable(params string[] columnNames)
    {
        return ToTable(null, columnNames);
    }

    public InterchangeData ToTable(string? tableName, params string[] columnNames)
    {
        if (columnNames == null)
            throw ExceptionBuilder.ArgumentNull(nameof(columnNames));

        InterchangeData dt = new InterchangeData();
        dt.Locale = Table!.Locale;
        dt.CaseSensitive = Table.CaseSensitive;
        dt.TableName = tableName ?? Table.TableName;
        dt.Namespace = Table.Namespace;
        dt.Prefix = Table.Prefix;
        dt.Metadata = _metadata;

        if (columnNames.Length == 0)
        {
            columnNames = new string[Table!.Columns.Count];
            for (int i = 0; i < columnNames.Length; i++)
            {
                columnNames[i] = Table.Columns[i].ColumnName;
            }
        }

        int[] columnIndexes = new int[columnNames.Length];

        List<object[]> rowlist = new List<object[]>();

        for (int i = 0; i < columnNames.Length; i++)
        {
            DataColumn? dc = Table!.Columns[columnNames[i]];
            if (dc == null)
            {
                throw ExceptionBuilder.ColumnNotInTheUnderlyingTable(columnNames[i], Table.TableName);
            }
            dt.Columns.Add(dc.ColumnName, dc.DataType);
            columnIndexes[i] = Table.Columns.IndexOf(dc);
        }

        foreach (var drview in GetDataRowViews())
        {
            object[] o = new object[columnNames.Length];

            for (int j = 0; j < columnIndexes.Length; j++)
            {
                o[j] = drview[columnIndexes[j]];
            }

            var dr = dt.NewRow();
            dr.ItemArray = o;
            dr.Metadata = drview.Metadata;

            dt.Rows.Add(dr);
            rowlist.Add(o);
        }

        return dt;
    }
}