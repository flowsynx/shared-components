namespace FlowSynx.Configuration;

public interface IConfigurationManager
{
    ConfigurationItem GetSetting(string name);
    IEnumerable<ConfigurationItem> GetSettings();
    bool IsExist(string name);
    ConfigurationStatus AddSetting(ConfigurationItem configuration);
    void DeleteSetting(string name);
}