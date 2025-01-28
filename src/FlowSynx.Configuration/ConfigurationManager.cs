using EnsureThat;
using FlowSynx.Configuration.Exceptions;
using FlowSynx.Configuration.Options;
using FlowSynx.IO.FileSystem;
using FlowSynx.IO.Serialization;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using FlowSynx.Data.Queries;
using FlowSynx.Data.Extensions;

namespace FlowSynx.Configuration;

public class ConfigurationManager : IConfigurationManager
{
    private readonly ILogger<ConfigurationManager> _logger;
    private readonly ConfigurationPath _options;
    private readonly IFileReader _fileReader;
    private readonly IFileWriter _fileWriter;
    private readonly ISerializer _serializer;
    private readonly IDeserializer _deserializer;
    private readonly IDataService _dataTableService;

    public ConfigurationManager(ILogger<ConfigurationManager> logger, ConfigurationPath options, 
        IFileReader fileReader, IFileWriter fileWriter, ISerializer serializer, 
        IDeserializer deserializer, IDataService dataTableService)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        EnsureArg.IsNotNull(options, nameof(options));
        EnsureArg.IsNotNull(fileReader, nameof(fileReader));
        EnsureArg.IsNotNull(fileWriter, nameof(fileWriter));
        EnsureArg.IsNotNull(serializer, nameof(serializer));
        EnsureArg.IsNotNull(deserializer, nameof(deserializer));
        EnsureArg.IsNotNull(dataTableService, nameof(dataTableService));
        _logger = logger;
        _options = options;
        _fileReader = fileReader;
        _fileWriter = fileWriter;
        _serializer = serializer;
        _deserializer = deserializer;
        _dataTableService = dataTableService;
    }

    public IEnumerable<object> List(ConfigurationListOptions listOptions)
    {
        var configurations = Configurations.Configurations.Select(x=>new ConfigListResponse
        {
            Id = x.Id,
            Name = x.Name,
            Type = x.Type,
            ModifiedTime = x.ModifiedTime,
        });

        var dataTable = configurations.ListToInterchangeData();
        var selectDataOption = new SelectDataOption()
        {
            Fields = listOptions.Fields,
            Filter = listOptions.Filter,
            Sort = listOptions.Sort,
            Paging = listOptions.Paging,
            CaseSensitive = listOptions.CaseSensitive,
        };

        var filteredData = _dataTableService.Select(dataTable, selectDataOption);
        return filteredData.DataTableToList();
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

    public IEnumerable<ConfigurationResult> Delete(ConfigurationListOptions listOptions)
    {
        var result = new List<ConfigurationResult>();
        listOptions.Fields?.Clear();
        var filteredList = List(listOptions);
        var configurationItems = filteredList.ToList();

        var configurations = Configurations.Configurations;

        if (configurationItems.Any())
        {
            foreach (var configurationItem in configurationItems)
            {
                var objExpando = configurationItem as ExpandoObject;
                var obj = objExpando as IDictionary<string, object>;

                var configItem = new ConfigurationItem
                {
                    Id = (Guid)obj["Id"],
                    Name = (string)obj["Name"],
                    Type = (string)obj["Type"],
                };

                configurations.Remove(configItem);
                result.Add(new ConfigurationResult(configItem.Id));
            }

            var newSetting = new Configuration()
            {
                Configurations = configurations!
            };

            var dataToWrite = _serializer.Serialize(newSetting);
            _fileWriter.Write(_options.Path, dataToWrite);

            return result;
        }

        _logger.LogWarning($"No setting found!");
        throw new ConfigurationException(Resources.ConfigurationManagerNotSettingFoumd);

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
        catch (Exception)
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