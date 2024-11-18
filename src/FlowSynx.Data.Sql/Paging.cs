using FlowSynx.Data.Sql.Exceptions;
using System.Text;

namespace FlowSynx.Data.Sql;

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
                throw new DataSqlException(Resources.SizeCouldNotBeNagative);

            _size = value ?? 0;
        }
    }

    public int? OffSet
    {
        get => _offSet;
        set
        {
            if (value <= 0)
                throw new DataSqlException(Resources.OffsetCouldNotBeNagative);

            _offSet = value ?? 0;
        }
    }

    public string GetQuery(Format format)
    {
        var sb = new StringBuilder();

        if (format.Type == SqlType.MsSql)
        {
            if (_offSet > 0)
            {
                sb.Append($"OFFSET {_offSet} ROWS");
            }

            if (_size > 0 && sb.Length > 0)
            {
                sb.Append(" ");
                sb.Append($"FETCH FIRST {_size} ROWS ONLY");
            }
        }
        else
        {
            if (_size > 0)
            {
                sb.Append($"LIMIT {_size}");
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