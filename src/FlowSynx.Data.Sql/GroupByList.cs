﻿using System.Text;

namespace FlowSynx.Data.Sql;

/// <summary>
/// Inspired by SqlBuilder open source project (https://github.com/koshovyi/SqlBuilder/tree/master)
/// </summary>
public class GroupByList : List<string>
{
    public string GetQuery(Format format)
    {
        var sb = new StringBuilder();

        var sep = false;
        foreach (var groupBy in this)
        {
            if (sep)
                sb.Append(", ");
            else
                sep = true;
            sb.Append(format.FormatField(groupBy));
        }

        return sb.ToString();
    }
}