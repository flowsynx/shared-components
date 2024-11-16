using FlowSynx.Data.SqlQuery;
using FlowSynx.Data.SqlQuery.Fetches;
using FlowSynx.Data.SqlQuery.Fields;
using FlowSynx.Data.SqlQuery.Filters;
using FlowSynx.Data.SqlQuery.Grouping;
using FlowSynx.Data.SqlQuery.Joins;
using FlowSynx.Data.SqlQuery.Sorting;
using FlowSynx.Data.SqlQuery.Tables;
using FlowSynx.Data.SqlQuery.Templates;

namespace FlowSynx.Data.SqlQuery.Queries;

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

    public string GetQuery()
    {
        var result = TemplateLibrary.Select;
        result.Append(SnippetLibrary.Table(Format, Table.Name, Table.Alias));
        result.Append(SnippetLibrary.Fields(Fields.GetQuery(Format, Table.Alias)));

        if (Joins is { Count: > 0 })
        {
            var joinTable = string.IsNullOrEmpty(Table.Alias) ? Table.Name : Table.Alias;
            result.Append(SnippetLibrary.Join(Joins.GetQuery(Format, joinTable)));
        }

        if (Filters is { Count: > 0 })
            result.Append(SnippetLibrary.Filters(Filters.GetQuery(Format, Table.Alias)));

        if (GroupBy is { Count: > 0 })
            result.Append(SnippetLibrary.GroupBy(GroupBy.GetQuery(Format, Table.Alias)));

        if (Sorts is { Count: > 0 })
            result.Append(SnippetLibrary.Sort(Sorts.GetQuery(Format, Table.Alias)));

        if (Fetch is not null)
            result.Append(SnippetLibrary.Fetch(Fetch.GetQuery(Format)));

        return result.GetQuery(Format);
    }

    public override string ToString()
    {
        return GetQuery();
    }
}
