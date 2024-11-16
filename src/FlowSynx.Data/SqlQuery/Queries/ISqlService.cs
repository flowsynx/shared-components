using System.Data;
using FlowSynx.Data.SqlQuery.Queries.Select;

namespace FlowSynx.Data.SqlQuery.Queries;

public interface ISqlService
{
    string Select(Format format, SelectSqlOption option);
}