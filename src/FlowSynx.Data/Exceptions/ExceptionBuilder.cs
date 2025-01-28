using System.Data;

namespace FlowSynx.Data.Exceptions;

public static class ExceptionBuilder
{
    public static ArgumentNullException ArgumentNull(string paramName)
    {
        throw new ArgumentNullException(paramName);
    }

    public static DataException ColumnNotInTheUnderlyingTable(string columnName, string tableName)
    {
        throw new DataException($"Column '{columnName}' not found in the table {tableName}.");
    }
}
