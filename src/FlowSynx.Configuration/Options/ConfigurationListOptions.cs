﻿using FlowSynx.Data;

namespace FlowSynx.Configuration.Options;

public class ConfigurationListOptions
{
    public FieldsList? Fields { get; set; }
    public FiltersList? Filters { get; set; }
    public SortsList? Sorts { get; set; }
    public Paging? Paging { get; set; }
    public bool? CaseSensitive { get; set; } = false;
}