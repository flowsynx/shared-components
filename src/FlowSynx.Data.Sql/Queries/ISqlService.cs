using FlowSynx.Data.Sql;

namespace FlowSynx.Data.SqlQuery.Queries;

public interface ISqlService
{
    string Select(Format format, SelectSqlOption option);
    string Insert(Format format, InsertSqlOption option);
}