﻿namespace FlowSynx.Data.Sql.Builder;

public class DeleteOption
{
    public required string Table { get; set; }
    public FilterList? Filter { get; set; } = new();
}
