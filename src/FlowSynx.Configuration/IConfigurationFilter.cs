namespace FlowSynx.Configuration;

public interface IConfigurationFilter
{
    IEnumerable<ConfigurationItem> FilterConfigurationList(IEnumerable<ConfigurationItem> entities, 
        ConfigurationSearchOptions searchOptions, ConfigurationListOptions listOptions);
}