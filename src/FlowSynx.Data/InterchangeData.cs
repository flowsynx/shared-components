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