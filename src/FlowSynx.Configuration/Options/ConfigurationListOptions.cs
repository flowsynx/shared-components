using FlowSynx.Data;

namespace FlowSynx.Configuration.Options;

public class ConfigurationListOptions
{
    public FieldsList? Fields { get; set; }
    public FilterList? Filter { get; set; }
    public SortList? Sort { get; set; }
    public Paging? Paging { get; set; }
    public bool? CaseSensitive { get; set; } = false;
}