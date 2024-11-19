using FlowSynx.Data.Sql;

namespace FlowSynx.Data.SqlQuery.Queries;

public class InsertSqlOption
{
    public required Table Table { get; set; }
    public FieldsList Fields { get; set; } = new();
    public ValueList Values { get; set; } = new();
}
