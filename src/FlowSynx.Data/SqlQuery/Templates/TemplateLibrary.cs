namespace FlowSynx.Data.SqlQuery.Templates;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public static class TemplateLibrary
{

    public static Template Select
    {
        get
        {
            var sql = "{{START}}SELECT {{FIELDS}} FROM {{TABLE}}{{JOINS}}{{FILTERS}}{{GROUPBY}}{{ORDERBY}}{{FETCHES}}{{END}}";
            return new Template(sql);
        }
    }
}