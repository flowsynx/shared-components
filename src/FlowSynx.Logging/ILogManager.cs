using FlowSynx.Logging.Options;

namespace FlowSynx.Logging;

public interface ILogManager
{
    IEnumerable<object> List(LogListOptions listOptions);
}