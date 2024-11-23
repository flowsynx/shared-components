namespace FlowSynx.Data.Sql.Templates;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public static class SnippetLibrary
{
    public static Snippet End(string value)
    {
        return new Snippet("END", value);
    }

    public static Snippet Table(Format format, string table)
    {
        table = format.FormatTable(table);
        return new Snippet("TABLE", table);
    }

    public static Snippet CreateTableFields(string value)
    {
        return new Snippet("CREATETABLEFIELDS", value, "(", ")");
    }

    public static Snippet Fields(string value)
    {
        return new Snippet("FIELDS", value);
    }

    public static Snippet Join(string value)
    {
        return new Snippet("JOINS", ' ' + value);
    }

    public static Snippet Values(string fields = "")
    {
        return new Snippet("VALUES", fields);
    }

    public static Snippet Filters(string value)
    {
        return new Snippet("FILTERS", value, " WHERE ");
    }

    public static Snippet GroupBy(string value)
    {
        return new Snippet("GROUPBY", value, " GROUP BY ");
    }

    public static Snippet Sort(string value)
    {
        return new Snippet("ORDERBY", value, " ORDER BY ");
    }

    public static Snippet Paging(string value)
    {
        return new Snippet("FETCHES", ' ' + value);
    }
}