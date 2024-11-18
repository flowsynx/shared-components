namespace FlowSynx.Data.Sql;

public class Table
{
    public required string Name { get; set; }
    public string? Alias { get; set; }

    public string GetQuery(Format format)
    {
        var tableAlias = "";
        var table = format.FormatTable(Name);
        if (!string.IsNullOrEmpty(Alias))
            tableAlias = format.FormatTableAlias(Alias);

        return string.IsNullOrEmpty(tableAlias)
            ? table
            : $"{table}{format.AliasOperator}{tableAlias}";
    }
}
