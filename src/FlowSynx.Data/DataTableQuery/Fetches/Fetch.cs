using FlowSynx.Data.SqlQuery.Exceptions;

namespace FlowSynx.Data.DataTableQuery.Fetches;

public class Fetch
{
    private int _limit = 0;

    public int? Limit
    {
        get => _limit;
        set
        {
            if (value <= 0)
                throw new DataSqlException(Resources.LimitCouldNotBeNagative);

            _limit = value ?? 0; ;
        }
    }

    public int GetQuery()
    {
        return _limit;
    }
}