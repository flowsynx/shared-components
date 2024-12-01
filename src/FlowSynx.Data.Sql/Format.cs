namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class Format
{
    protected Dictionary<Type, string> TypeMap = new Dictionary<Type, string>();
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

    public string GetDbType(Type? giveType)
    {
        if (giveType == null)
            return TypeMap[typeof(string)];

        giveType = Nullable.GetUnderlyingType(giveType) ?? giveType;

        if (TypeMap.ContainsKey(giveType))
            return TypeMap[giveType];
        
        throw new ArgumentException($"{giveType.FullName} is not a supported .NET class");
    }

    public string GetDbType<T>()
    {
        return GetDbType(typeof(T));
    }

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
        TypeMap = new Dictionary<Type, string>
        {
            [typeof(string)] = "LONGTEXT",
            [typeof(char[])] = "VARCHAR",
            [typeof(byte)] = "tinyint",
            [typeof(short)] = "smallint",
            [typeof(int)] = "int",
            [typeof(long)] = "bigint",
            [typeof(byte[])] = "image",
            [typeof(bool)] = "bit",
            [typeof(DateTime)] = "datetime2",
            [typeof(DateTimeOffset)] = "datetimeoffset",
            [typeof(decimal)] = "money",
            [typeof(float)] = "real",
            [typeof(double)] = "float",
            [typeof(TimeSpan)] = "time"
        }
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
        TypeMap = new Dictionary<Type, string>
        {
            [typeof(string)] = "nvarchar",
            [typeof(char[])] = "nvarchar",
            [typeof(byte)] = "tinyint",
            [typeof(short)] = "smallint",
            [typeof(int)] = "int",
            [typeof(long)] = "bigint",
            [typeof(byte[])] = "image",
            [typeof(bool)] = "bit",
            [typeof(DateTime)] = "datetime2",
            [typeof(DateTimeOffset)] = "datetimeoffset",
            [typeof(decimal)] = "money",
            [typeof(float)] = "real",
            [typeof(double)] = "float",
            [typeof(TimeSpan)] = "time"
        }
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
        TypeMap = new Dictionary<Type, string>
        {
            [typeof(string)] = "nvarchar",
            [typeof(char[])] = "nvarchar",
            [typeof(byte)] = "tinyint",
            [typeof(short)] = "smallint",
            [typeof(int)] = "int",
            [typeof(long)] = "bigint",
            [typeof(byte[])] = "image",
            [typeof(bool)] = "bit",
            [typeof(DateTime)] = "datetime2",
            [typeof(DateTimeOffset)] = "datetimeoffset",
            [typeof(decimal)] = "money",
            [typeof(float)] = "real",
            [typeof(double)] = "float",
            [typeof(TimeSpan)] = "time"
        }
    };
}