using FlowSynx.Data.SqlQuery.Queries.Select;
using FlowSynx.Data.SqlQuery.Templates;

namespace FlowSynx.Data.SqlQuery.Queries;

public class SqlService : ISqlService
{
    public string Select(Format format, SelectSqlOption option)
    {
        var result = TemplateLibrary.Select;
        result.Append(SnippetLibrary.Table(format, option.Table.Name, option.Table.Alias));
        result.Append(SnippetLibrary.Fields(option.Fields.GetQuery(format, option.Table.Alias)));

        if (option.Joins is { Count: > 0 })
        {
            var joinTable = string.IsNullOrEmpty(option.Table.Alias) ? option.Table.Name : option.Table.Alias;
            result.Append(SnippetLibrary.Join(option.Joins.GetQuery(format, joinTable)));
        }

        if (option.Filters is { Count: > 0 })
            result.Append(SnippetLibrary.Filters(option.Filters.GetQuery(format, option.Table.Alias)));

        if (option.GroupBy is { Count: > 0 })
            result.Append(SnippetLibrary.GroupBy(option.GroupBy.GetQuery(format, option.Table.Alias)));

        if (option.Sorts is { Count: > 0 })
            result.Append(SnippetLibrary.Sort(option.Sorts.GetQuery(format, option.Table.Alias)));

        if (option.Paging is not null)
            result.Append(SnippetLibrary.Paging(option.Paging.GetQuery(format)));

        return result.GetQuery(format);
    }
}
