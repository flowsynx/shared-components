namespace FlowSynx.Data.Sql;

public static class FormatExtensions
{
    public static string FormatField(this Format format, string field)
    {
        return format.EscapeEnabled
            ? format.ColumnEscapeLeft + field + format.ColumnEscapeRight
            : field;
    }

    public static string FormatTable(this Format format, string tableName)
    {
        return format.EscapeEnabled
            ? format.TableEscapeLeft + tableName + format.TableEscapeRight
            : tableName;
    }
}