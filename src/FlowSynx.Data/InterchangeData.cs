using FlowSynx.Data.Extensions;
using System.Data;

namespace FlowSynx.Data;

public class InterchangeMetadata
{
    public string? Content { get; set; }
}

public class InterchangeRowMetadata
{
    public string? Key { get; set; }
    public string? ContentType { get; set; }
    public string? Content { get; set; }
    public string? ContentHash { get; set; }
}

public class InterchangeData : DataTable
{
    public InterchangeMetadata Metadata { get; set; }

    public InterchangeData(): this(Guid.NewGuid().ToString())
    {
        Metadata = new InterchangeMetadata();
    }

    public InterchangeData(string name)
    {
        TableName = name;
        Metadata = new InterchangeMetadata();
    }

    public void ImportMetadata(InterchangeRow row)
    {
        row.Metadata.CopyProperties(this.Metadata);
    }

    protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
    {
        return new InterchangeRow(builder);
    }

    public new InterchangeRow NewRow()
    {
        return (InterchangeRow)base.NewRow();
    }

    public IEnumerable<InterchangeRow> AsEnumerable()
    {
        foreach (InterchangeRow row in base.Rows)
        {
            yield return row;
        }
    }

    public new InterchangeDataView DefaultView => new(this);

    public override DataTable Clone()
    {
        var clone = (InterchangeData)base.Clone();
        return clone;
    }

    public InterchangeData CopyWithMetadata()
    {
        InterchangeData newTable = (InterchangeData)this.Clone(); // Clone structure

        foreach (InterchangeRow originalRow in Rows)
        {
            InterchangeRow newRow = (InterchangeRow)newTable.NewRow();
            newRow.ItemArray = originalRow.ItemArray; // Copy column values

            // Copy metadata manually
            newRow.Metadata = originalRow.Metadata;
            newTable.ImportRow(newRow); // Import row into new table
        }

        return newTable;
    }

    public InterchangeData FromList(List<InterchangeRow> rowList)
    {
        if (rowList == null || rowList.Count == 0) return new InterchangeData();

        InterchangeData newTable = new InterchangeData();
        newTable = (InterchangeData)rowList[0].Table.Clone(); // Copy schema

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
}

public class InterchangeRow : DataRow
{
    public InterchangeRowMetadata Metadata { get; set; }

    public InterchangeRow(DataRowBuilder builder) : base(builder)
    {
        Metadata = new InterchangeRowMetadata();
    }

    public new InterchangeData Table => (InterchangeData)base.Table;

    public void ImportMetadata(InterchangeRow row)
    {
        row.Metadata.CopyProperties(this.Metadata);
    }
}