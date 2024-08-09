using FlowSynx.Configuration.Options;

namespace FlowSynx.Configuration.Filters;

public interface IConfigurationFilter
{
    IEnumerable<ConfigurationItem> FilterConfigurationList(IEnumerable<ConfigurationItem> entities,
        ConfigurationSearchOptions searchOptions, ConfigurationListOptions listOptions);
}