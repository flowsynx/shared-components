using FlowSynx.Data.SqlQuery.Exceptions;
using System.Text;

namespace FlowSynx.Data.SqlQuery.Fetches;

public class Fetch
{
    private int _limit = 0;
    private int _offSet = 0;

    public int? Limit
    {
        get
        {
            return _limit;
        }

        set
        {
            if (value <= 0)
                throw new DataSqlException(Resources.LimitCouldNotBeNagative);

            _limit = value ?? 0; ;
        }
    }

    public int? OffSet
    {
        get
        {
            return _offSet;
        }

        set
        {
            if (value <= 0)
                throw new DataSqlException(Resources.OffsetCouldNotBeNagative);

            _offSet = value ?? 0; ;
        }
    }

    public string GetSql(Format format)
    {
        var sb = new StringBuilder();

        if (format.Type == SqlType.MsSql)
        {
            if (_offSet > 0)
            {
                sb.Append($"OFFSET {_offSet} ROWS");
            }

            if (_limit > 0 && sb.Length > 0)
            {
                sb.Append(" ");
                sb.Append($"FETCH FIRST {_limit} ROWS ONLY");
            }
        }
        else
        {
            if (_limit > 0)
            {
                sb.Append($"LIMIT {_limit}");
            }

            if (_offSet > 0 && sb.Length > 0)
            {
                sb.Append(" ");
                sb.Append($"OFFSET {_offSet}");
            }
        }
        return sb.ToString();
    }
}