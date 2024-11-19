using System.Text;

namespace FlowSynx.Data;

public class FilterList : List<Filter>
{
    private string GetLogicOperator(LogicOperator? filterOperator)
    {
        return filterOperator switch
        {
            LogicOperator.AndNot => "AND NOT",
            LogicOperator.Or => "OR",
            LogicOperator.And => "AND",
            _ => "AND"
        };
    }

    public string GetQuery()
    {
        var sb = new StringBuilder();
        foreach (var filter in this)
        {
            if (sb.Length > 0)
            {
                sb.Append(' ');
                sb.Append(GetLogicOperator(filter.Logic));
                sb.Append(' ');
            }

            sb.Append(filter.GetQuery());
        }

        return sb.ToString();
    }
}