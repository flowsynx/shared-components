namespace FlowSynx.Data.Sql;

public static class FormatExtensions
{
    public static string FormatField(this Format format, string field, string? tableAlias = "")
    {
        if (!string.IsNullOrEmpty(tableAlias))
            tableAlias = format.FormatTableAlias(tableAlias) + '.';

        field = format.EscapeEnabled
            ? format.ColumnEscapeLeft + field + format.ColumnEscapeRight
            : field;

        return tableAlias + field;
    }

    public static string FormatTable(this Format format, string tableName)
    {
        return format.EscapeEnabled
            ? format.TableEscapeLeft + tableName + format.TableEscapeRight
            : tableName;
    }

    public static string FormatTableAlias(this Format format, string value)
    {
        return format.TableEscapeLeft + value + format.TableEscapeRight;
    }
}