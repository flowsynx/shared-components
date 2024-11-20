using FlowSynx.Data.Sql.Templates;

namespace FlowSynx.Data.Sql.Builder;

public class SqlBuilder : ISqlBuilder
{
    public string Insert(Format format, InsertOption option)
    {
        Template result = TemplateLibrary.Insert;
        result.Append(SnippetLibrary.Table(format, option.Table.Name, option.Table.Alias));
        result.Append(SnippetLibrary.Fields(option.Fields.GetQuery(format, option.Table.Alias)));
        result.Append(SnippetLibrary.Values(option.Values.GetQuery()));

        return result.GetQuery(format);
    }

    public string Select(Format format, SelectOption option)
    {
        var result = TemplateLibrary.Select;
        result.Append(SnippetLibrary.Table(format, option.Table.Name, option.Table.Alias));
        result.Append(SnippetLibrary.Fields(option.Fields.GetQuery(format, option.Table.Alias)));

        if (option.Join is { Count: > 0 })
        {
            var joinTable = string.IsNullOrEmpty(option.Table.Alias) ? option.Table.Name : option.Table.Alias;
            result.Append(SnippetLibrary.Join(option.Join.GetQuery(format, joinTable)));
        }

        if (option.Filter is { Count: > 0 })
            result.Append(SnippetLibrary.Filters(option.Filter.GetQuery(format, option.Table.Alias)));

        if (option.GroupBy is { Count: > 0 })
            result.Append(SnippetLibrary.GroupBy(option.GroupBy.GetQuery(format, option.Table.Alias)));

        if (option.Sort is { Count: > 0 })
            result.Append(SnippetLibrary.Sort(option.Sort.GetQuery(format, option.Table.Alias)));

        if (option.Paging is not null)
            result.Append(SnippetLibrary.Paging(option.Paging.GetQuery(format)));

        return result.GetQuery(format);
    }
}
