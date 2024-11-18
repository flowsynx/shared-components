using FlowSynx.Data.DataTableQuery.Extensions.Exceptions;

namespace FlowSynx.Data.DataTableQuery.Pagination;

public class Paging
{
    private int _size;
    private int _offSet;

    public int? Size
    {
        get => _size;
        set
        {
            if (value <= 0)
                throw new DataTableQueryException(Resources.SizeCouldNotBeNagative);

            _size = value ?? 0;
        }
    }

    public int? OffSet
    {
        get => _offSet;
        set
        {
            if (value < 0)
                throw new DataTableQueryException(Resources.OffsetCouldNotBeNagative);

            _offSet = value ?? 0;
        }
    }
}