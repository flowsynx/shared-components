namespace FlowSynx.Connectors.Abstractions;

public class TransferData
{
    public required Namespace Namespace { get; set; }
    public required string ConnectorType { get; set; }
    public string? ContentType { get; set; }
    public string? Content { get; set; }
    public required IEnumerable<TransferDataColumn> Columns { get; set; }
    public required IEnumerable<TransferDataRow> Rows { get; set; }
}

public class TransferDataColumn
{
    public required string Name { get; set; }
    public Type? DataType { get; set; } = typeof(string);
}

public class TransferDataRow
{
    public required string Key { get; set; }
    public string? ContentType { get; set; }
    public string? Content { get; set; }
    public object?[]? Items { get; set; }
}