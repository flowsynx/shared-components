﻿using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
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

    public string GetQuery(Format format)
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

            sb.Append(filter.GetQuery(format));
        }

        if (Count > 0) 
        { 
            sb.Insert(0, "(");
            sb.Insert(sb.Length, ")");
        }

        return sb.ToString();
    }
}