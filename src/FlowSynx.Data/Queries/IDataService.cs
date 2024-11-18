using System.Data;

namespace FlowSynx.Data.Queries;

public interface IDataService
{
    DataTable Select(DataTable dataTable, SelectDataOption option);
}