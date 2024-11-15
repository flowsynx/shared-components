using FlowSynx.Data.Sql.Fetches;
using FlowSynx.Data.Sql.Fields;
using FlowSynx.Data.Sql.Filters;
using FlowSynx.Data.Sql.Grouping;
using FlowSynx.Data.Sql.Joins;
using FlowSynx.Data.Sql.Sorting;
using FlowSynx.Data.Sql.Tables;
using FlowSynx.Data.Sql.Templates;

namespace FlowSynx.Data.Sql.Queries;

public class SelectStatement
{
    public Format Format { get; }
    public Table Table { get; }
    public FieldsList Fields { get; set; }
    public JoinsList? Joins { get; set; }
    public FiltersList? Filters { get; set; }
    public GroupByList? GroupBy { get; set; }
    public SortsList? Sorts { get; set; }
    public Fetch? Fetch { get; set; }

    public SelectStatement(Format format, Table table)
    {
        Format = format;
        Table = table;
        Fields = new FieldsList();
        Joins = new JoinsList();
        Filters = new FiltersList();
        GroupBy = new GroupByList();
        Sorts = new SortsList();
        Fetch = new Fetch();
    }

    public string GetSql()
    {
        var result = TemplateLibrary.Select;
        result.Append(SnippetLibrary.Table(Format, Table.Name, Table.Alias));
        result.Append(SnippetLibrary.Fields(Fields.GetSql(Format, Table.Alias)));

        if (Joins is { Count: > 0 })
        {
            var joinTable = string.IsNullOrEmpty(Table.Alias) ? Table.Name : Table.Alias;
            result.Append(SnippetLibrary.Join(Joins.GetSql(Format, joinTable)));
        }

        if (Filters is { Count: > 0 })
            result.Append(SnippetLibrary.Filters(Filters.GetSql(Format, Table.Alias)));

        if (GroupBy is { Count: > 0 })
            result.Append(SnippetLibrary.GroupBy(GroupBy.GetSql(Format, Table.Alias)));

        if (Sorts is { Count: > 0 })
            result.Append(SnippetLibrary.Sort(Sorts.GetSql(Format, Table.Alias)));

        if (Fetch is not null)
            result.Append(SnippetLibrary.Fetch(Fetch.GetSql(Format)));

        return result.GetSql(Format);
    }
    
    public override string ToString()
    {
        return GetSql();
    }
}
