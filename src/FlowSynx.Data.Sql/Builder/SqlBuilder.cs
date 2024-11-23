using FlowSynx.Data.Sql.Templates;

namespace FlowSynx.Data.Sql.Builder;

public class SqlBuilder : ISqlBuilder
{
    public string Create(Format format, CreateOption option)
    {
        Template result = TemplateLibrary.CreateTable;
        result.Append(SnippetLibrary.Table(format, option.Name));
        result.Append(SnippetLibrary.CreateTableFields(option.Fields.GetQuery(format)));

        return result.GetQuery(format);
    }

    public string Select(Format format, SelectOption option)
    {
        var result = TemplateLibrary.Select;
        result.Append(SnippetLibrary.Table(format, option.Table));
        result.Append(SnippetLibrary.Fields(option.Fields.GetQuery(format)));

        if (option.Join is { Count: > 0 })
            result.Append(SnippetLibrary.Join(option.Join.GetQuery(format, option.Table)));

        if (option.Filter is { Count: > 0 })
            result.Append(SnippetLibrary.Filters(option.Filter.GetQuery(format)));

        if (option.GroupBy is { Count: > 0 })
            result.Append(SnippetLibrary.GroupBy(option.GroupBy.GetQuery(format)));

        if (option.Sort is { Count: > 0 })
            result.Append(SnippetLibrary.Sort(option.Sort.GetQuery(format)));

        if (option.Paging is not null)
            result.Append(SnippetLibrary.Paging(option.Paging.GetQuery(format)));

        return result.GetQuery(format);
    }

    public string Insert(Format format, InsertOption option)
    {
        option.Fields.ForEach(x => x.Alias = null);
        Template result = TemplateLibrary.Insert;
        result.Append(SnippetLibrary.Table(format, option.Table));
        result.Append(SnippetLibrary.Fields(option.Fields.GetQuery(format)));
        result.Append(SnippetLibrary.Values(option.Values.GetQuery()));

        return result.GetQuery(format);
    }
}
