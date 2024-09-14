using System.Data;

namespace FlowSynx.Plugin.Abstractions.Filter;

public interface IDataFilter
{
    DataTable Filter(DataTable dataTable, DataFilterOptions? dataFilterOptions);
}