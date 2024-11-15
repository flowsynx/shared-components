namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class Format
{
    public SqlType Type { get; set; } = SqlType.Unknown;
    public char Parameter { get; set; }
    public bool EscapeEnabled { get; set; }
    public char ColumnEscapeLeft { get; set; }
    public char ColumnEscapeRight { get; set; }
    public char TableEscapeLeft { get; set; }
    public char TableEscapeRight { get; set; }
    public char EndOfStatement { get; set; }
    public char AliasEscape { get; set; }
    public string AliasOperator { get; set; } = string.Empty;

    /// <summary>
    /// MySQL configuration
    /// </summary>
    public static Format MySql => new Format()
    {
        Type = SqlType.MySql,
        EscapeEnabled = true,
        TableEscapeLeft = '`',
        TableEscapeRight = '`',
        ColumnEscapeLeft = '`',
        ColumnEscapeRight = '`',
        Parameter = '?',
        EndOfStatement = ';',
        AliasEscape = '\"',
        AliasOperator = " as ",
    };

    /// <summary>
    /// Microsoft SQL Server configuration
    /// </summary>
    public static Format MsSql => new Format()
    {
        Type = SqlType.MsSql,
        EscapeEnabled = true,
        TableEscapeLeft = '[',
        TableEscapeRight = ']',
        ColumnEscapeLeft = '[',
        ColumnEscapeRight = ']',
        Parameter = '@',
        EndOfStatement = ';',
        AliasEscape = '\'',
        AliasOperator = " as ",
    };

    /// <summary>
    /// PostgreSQL configuration
    /// </summary>
    public static Format PostgreSql => new Format()
    {
        Type = SqlType.PostgreSql,
        EscapeEnabled = false,
        TableEscapeLeft = '\0',
        TableEscapeRight = '\0',
        ColumnEscapeLeft = '\0',
        ColumnEscapeRight = '\0',
        Parameter = '@',
        EndOfStatement = ';',
        AliasEscape = '\0',
        AliasOperator = " as ",
    };
}