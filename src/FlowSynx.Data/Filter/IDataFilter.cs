using System.Data;

namespace FlowSynx.Data.Filter;

public interface IDataFilter
{
    DataTable Filter(DataTable dataTable, DataFilterOptions? dataFilterOptions);
}