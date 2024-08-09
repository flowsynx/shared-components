using EnsureThat;
using FlowSynx.Configuration.Exceptions;
using FlowSynx.Configuration.Filters;
using FlowSynx.Configuration.Options;
using FlowSynx.IO.FileSystem;
using FlowSynx.IO.Serialization;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Configuration;

public class ConfigurationManager : IConfigurationManager
{
    private readonly ILogger<ConfigurationManager> _logger;
    private readonly ConfigurationPath _options;
    private readonly IFileReader _fileReader;
    private readonly IFileWriter _fileWriter;
    private readonly ISerializer _serializer;
    private readonly IDeserializer _deserializer;
    private readonly IConfigurationFilter _configurationFilter;

    public ConfigurationManager(ILogger<ConfigurationManager> logger, ConfigurationPath options, 
        IFileReader fileReader, IFileWriter fileWriter, ISerializer serializer, 
        IDeserializer deserializer, IConfigurationFilter configurationFilter)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(options, nameof(options));
        EnsureArg.IsNotNull(fileReader, nameof(fileReader));
        EnsureArg.IsNotNull(fileWriter, nameof(fileWriter));
        EnsureArg.IsNotNull(serializer, nameof(serializer));
        EnsureArg.IsNotNull(deserializer, nameof(deserializer));
        _logger = logger;
        _options = options;
        _fileReader = fileReader;
        _fileWriter = fileWriter;
        _serializer = serializer;
        _deserializer = deserializer;
        _configurationFilter = configurationFilter;
    }

    public IEnumerable<ConfigurationItem> List(ConfigurationSearchOptions searchOptions,
        ConfigurationListOptions listOptions)
    {
        var result = new List<ConfigurationItem>();
        var contents = _fileReader.Read(_options.Path);
        var deserializeResult = _deserializer.Deserialize<Configuration>(contents, new JsonSerializationConfiguration() { NameCaseInsensitive = false });

        return _configurationFilter.FilterConfigurationList(deserializeResult.Configurations, searchOptions, listOptions);
    }

    public ConfigurationResult Add(ConfigurationItem configuration)
    {
        var configurations = Configurations.Configurations;
        configurations.Add(configuration);

        var newSetting = new Configuration() { Configurations = configurations! };
        var dataToWrite = _serializer.Serialize(newSetting);
        _fileWriter.Write(_options.Path, dataToWrite);
        return new ConfigurationResult(configuration.Id);
    }

    public ConfigurationResult Delete(string name)
    {
        var configurations = Configurations.Configurations;

        if (configurations is null)
        {
            _logger.LogWarning($"No setting found!");
            throw new ConfigurationException(Resources.ConfigurationManagerNotSettingFoumd);
        }

        var item = Get(name);
        configurations.Remove(item);
        var newSetting = new Configuration()
        {
            Configurations = configurations!
        };

        var dataToWrite = _serializer.Serialize(newSetting);
        _fileWriter.Write(_options.Path, dataToWrite);
        return new ConfigurationResult(item.Id);
    }

    public IEnumerable<ConfigurationResult> Delete(ConfigurationSearchOptions searchOptions,
        ConfigurationListOptions listOptions)
    {
        var result = new List<ConfigurationResult>();
        var filteredList = List(searchOptions, listOptions);
        foreach (var item in filteredList)
        {
            Delete(item.Name);
            result.Add(new ConfigurationResult(item.Id));
        }
        return result;
    }

    public ConfigurationItem Get(string name)
    {
        var configurations = Configurations;
        var result = configurations.Configurations
            .FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (result != null) return result;
        _logger.LogWarning($"{name} is not found.");
        throw new ConfigurationException(string.Format(Resources.ConfigurationManagerItemNotFoumd, name));
    }
    
    public bool IsExist(string name)
    {
        try
        {
            Get(name);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private Configuration Configurations
    {
        get
        {
            var contents = _fileReader.Read(_options.Path);

            var data = _deserializer.Deserialize<Configuration>(contents,
                new JsonSerializationConfiguration
                {
                    NameCaseInsensitive = false
                });

            return data ?? new Configuration();
        }
    }
}