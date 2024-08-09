using FlowSynx.Configuration.Options;

namespace FlowSynx.Configuration;

public interface IConfigurationManager
{
    IEnumerable<ConfigurationItem> List(ConfigurationSearchOptions searchOptions,
        ConfigurationListOptions listOptions);

    ConfigurationItem Get(string name);
    
    bool IsExist(string name);

    ConfigurationResult Add(ConfigurationItem configuration);

    ConfigurationResult Delete(string name);

    IEnumerable<ConfigurationResult> Delete(ConfigurationSearchOptions searchOptions, ConfigurationListOptions listOptions);
}