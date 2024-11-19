using FlowSynx.Data;

namespace FlowSynx.Logging.Options;

public class LogListOptions
{
    public FieldsList? Fields { get; set; }
    public FilterList? Filter { get; set; }
    public SortList? Sort { get; set; }
    public Paging? Paging { get; set; }
    public bool? CaseSensitive { get; set; } = false;
}