using FlowSynx.Data.SqlQuery.Exceptions;

namespace FlowSynx.Data.DataTableQuery.Pagination;

public class Paging
{
    private int _size = 0;

    public int? Size
    {
        get => _size;
        set
        {
            if (value <= 0)
                throw new DataSqlException(Resources.SizeCouldNotBeNagative);

            _size = value ?? 0; ;
        }
    }

    public int GetQuery()
    {
        return _size;
    }
}