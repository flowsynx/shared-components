using FlowSynx.Logging.Options;

namespace FlowSynx.Logging.Filters;

public interface ILogFilter
{
    IEnumerable<LogMessage> FilterLogsList(IEnumerable<LogMessage> logs,
        LogSearchOptions searchOptions, LogListOptions listOptions);
}