﻿using System.Text;

namespace FlowSynx.Data.DataTableQuery.Fields;

public class Field
{
    public required string Name { get; set; }

    public string GetQuery()
    {
        var sb = new StringBuilder();
        sb.Append(Name);
        return sb.ToString();
    }
}
