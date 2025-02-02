using FlowSynx.Data.Extensions;
using System.Data;

namespace FlowSynx.Data.Queries;

public class DataService : IDataService
{
    public InterchangeData Select(InterchangeData dataTable, SelectDataOption option)
    {
        dataTable.CaseSensitive = option.CaseSensitive ?? false;
        var view = dataTable.DefaultView;

        if (option.Filter is { Count: > 0 })
            view.RowFilter = option.Filter.GetQuery();

        if (option.Sort is { Count: > 0 })
            view.Sort = option.Sort.GetQuery();

        var result = option.Fields is { Count: > 0 }
            ? view.ToTable(option.Fields.GetQuery())
            : view.ToTable();

        if (option.Paging is not null)
        {
            IEnumerable<InterchangeRow> list = result.AsEnumerable();
            if (option.Paging.OffSet.HasValue && option.Paging.OffSet > 0)
            {
                if (option.Paging.OffSet.Value >= list.Count())
                    throw new FlowSynx.Data.Exceptions.DataException(string.Format(Resources.OffsetCannotBeGreaterThanTheTotalNumberOfEntities, list.Count()));

                list = list.Skip(option.Paging.OffSet.Value);
            }

            if (option.Paging.Size.HasValue && option.Paging.Size > 0)
                list = list.Take(option.Paging.Size.Value);

            result = list.ToList().CopyToInterchangeData(dataTable.Metadata);
        }

        return result;
    }
}