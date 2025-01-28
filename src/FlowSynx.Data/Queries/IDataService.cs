using System.Data;

namespace FlowSynx.Data.Queries;

public interface IDataService
{
    InterchangeData Select(InterchangeData dataTable, SelectDataOption option);
}