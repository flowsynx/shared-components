using System.Data;
using FlowSynx.Data.DataTableQuery.Queries.Select;

namespace FlowSynx.Data.DataTableQuery.Queries;

public interface IDataTableService
{
    DataTable Select(DataTable dataTable, SelectDataTableOption option);
}