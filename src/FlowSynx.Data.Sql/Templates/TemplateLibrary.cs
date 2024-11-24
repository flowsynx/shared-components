namespace FlowSynx.Data.Sql.Templates;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public static class TemplateLibrary
{
    public static Template CreateTable
    {
        get
        {
            string sql = "{{START}}CREATE TABLE {{TABLE}} {{CREATETABLEFIELDS}}{{END}}";
            return new Template(sql);
        }
    }

    public static Template Select
    {
        get
        {
            var sql = "{{START}}SELECT {{FIELDS}} FROM {{TABLE}}{{JOINS}}{{FILTERS}}{{GROUPBY}}{{ORDERBY}}{{FETCHES}}{{END}}";
            return new Template(sql);
        }
    }

    public static Template Insert
    {
        get
        {
            string sql = "{{START}}INSERT INTO {{TABLE}}({{FIELDS}}) VALUES({{VALUES}}){{END}}";
            return new Template(sql);
        }
    }

    public static Template Delete
    {
        get
        {
            string sql = "{{START}}DELETE FROM {{TABLE}}{{FILTERS}}{{END}}";
            return new Template(sql);
        }
    }
}