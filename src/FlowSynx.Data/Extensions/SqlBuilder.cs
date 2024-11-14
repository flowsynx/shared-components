using FlowSynx.Data.Sql;

namespace FlowSynx.Data.Extensions;

public static class SqlBuilder
{
    public static string FormatField(this ISqlFormat format, string field, string? tableAlias = "")
    {
        if (!string.IsNullOrEmpty(tableAlias))
            tableAlias = FormatTableAlias(format, tableAlias) + '.';

        field = format.EscapeEnabled
            ? format.ColumnEscapeLeft + field + format.ColumnEscapeRight
            : field;

        return tableAlias + field;
    }

    public static string FormatTable(this ISqlFormat format, string tableName)
    {
        return format.EscapeEnabled
            ? format.TableEscapeLeft + tableName + format.TableEscapeRight
            : tableName;
    }

    public static string FormatTableAlias(this ISqlFormat format, string value)
    {
        return format.TableEscapeLeft + value + format.TableEscapeRight;
    }
}