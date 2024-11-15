using FlowSynx.Data.Extensions;

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
    
    public static Snippet Table(Format format, string table, string? tableAlias = "")
    {
        table = format.FormatTable(table);
        if (!string.IsNullOrEmpty(tableAlias))
            tableAlias = format.FormatTableAlias(tableAlias);

        return string.IsNullOrEmpty(tableAlias) 
            ? new Snippet("TABLE", table) 
            : new Snippet("TABLE", table + format.AliasOperator + tableAlias);
    }

    public static Snippet Fields(string value)
    {
        return new Snippet("FIELDS", value);
    }

    public static Snippet Join(string value)
    {
        return new Snippet("JOINS", ' ' + value);
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

    public static Snippet Fetch(string value)
    {
        return new Snippet("FETCHES", ' ' + value);
    }
}