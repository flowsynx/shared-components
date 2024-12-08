using FlowSynx.Data.Sql.Templates;
using System.Text;

namespace FlowSynx.Data.Sql.Builder;

public class SqlBuilder : ISqlBuilder
{
    public string Create(Format format, CreateOption option)
    {
        var result = TemplateLibrary.CreateTable;
        result.Append(SnippetLibrary.Table(format, option.Table));
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

    public string ExistRecord(Format format, ExistRecordOption option)
    {
        var result = TemplateLibrary.Select;
        result.Append(SnippetLibrary.Table(format, option.Table));

        if (option.Filter is { Count: > 0 })
            result.Append(SnippetLibrary.Filters(option.Filter.GetQuery(format)));

        return result.GetQuery(format);
    }

    public string ExistTable(Format format, ExistTableOption option)
    {
        var result = TemplateLibrary.ExistTable;
        result.Append(SnippetLibrary.Table(format, option.Table));

        return result.GetQuery(format);
    }

    public string Insert(Format format, InsertOption option)
    {
        var result = TemplateLibrary.Insert;
        result.Append(SnippetLibrary.Table(format, option.Table));
        result.Append(SnippetLibrary.Fields(option.Fields.GetQuery(format)));
        result.Append(SnippetLibrary.Values(option.Values.GetQuery()));

        return result.GetQuery(format);
    }

    public string BulkInsert(Format format, BulkInsertOption option)
    {
        var result = TemplateLibrary.BulkInsert;
        result.Append(SnippetLibrary.Table(format, option.Table));
        result.Append(SnippetLibrary.Fields(option.Fields.GetQuery(format)));

        var index = 1;
        var sb = new StringBuilder();
        foreach (var item in option.Values)
        {
            sb.Append('(');
            sb.Append(item.GetQuery());
            sb.Append(')');

            if (index != option.Values.Count)
                sb.Append(", ");
            index++;
        }

        result.Append(SnippetLibrary.Values(sb.ToString()));

        return result.GetQuery(format);
    }

    public string Delete(Format format, DeleteOption option)
    {
        var result = TemplateLibrary.Delete;
        result.Append(SnippetLibrary.Table(format, option.Table));

        if (option.Filter is { Count: > 0 })
            result.Append(SnippetLibrary.Filters(option.Filter.GetQuery(format)));

        return result.GetQuery(format);
    }

    public string DropTable(Format format, DropTableOption option)
    {
        var result = TemplateLibrary.DropTable;
        result.Append(SnippetLibrary.Table(format, option.Table));

        return result.GetQuery(format);
    }

    public string TableFields(Format format, TableFieldsOption option)
    {
        var result = TemplateLibrary.TableFields;
        result.Append(SnippetLibrary.Table(format, option.Table));

        return result.GetQuery(format);
    }
}