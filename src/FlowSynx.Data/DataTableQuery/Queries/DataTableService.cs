﻿using System.Data;
using FlowSynx.Data.DataTableQuery.Extensions.Exceptions;
using FlowSynx.Data.DataTableQuery.Queries.Select;

namespace FlowSynx.Data.DataTableQuery.Queries;

public class DataTableService : IDataTableService
{
    public DataTable Select(DataTable dataTable, SelectDataTableOption option)
    {
        dataTable.CaseSensitive = option.CaseSensitive ?? false;
        var view = dataTable.DefaultView;

        if (option.Filters is { Count: > 0 })
            view.RowFilter = option.Filters.GetQuery();

        if (option.Sorts is { Count: > 0 })
            view.Sort = option.Sorts.GetQuery();

        var result = option.Fields is { Count: > 0 }
            ? view.ToTable(false, option.Fields.GetQuery())
            : view.ToTable(false);

        if (option.Paging is not null)
        {
            IEnumerable<DataRow> list = result.AsEnumerable();
            if (option.Paging.OffSet.HasValue && option.Paging.OffSet > 0)
            {
                if (option.Paging.OffSet.Value >= list.Count())
                    throw new DataTableQueryException(string.Format(Resources.OffsetCannotBeGreaterThanTheTotalNumberOfEntities, list.Count()));

                list = list.Skip(option.Paging.OffSet.Value);
            }

            if (option.Paging.Size.HasValue && option.Paging.Size > 0)
                list = list.Take(option.Paging.Size.Value);

            result = list.CopyToDataTable();
        }

        return result;
    }
}