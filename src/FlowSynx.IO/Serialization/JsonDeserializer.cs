using EnsureThat;
using FlowSynx.IO.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlowSynx.IO.Serialization;

public class JsonDeserializer : IDeserializer
{
    private readonly ILogger<JsonDeserializer> _logger;

    public JsonDeserializer(ILogger<JsonDeserializer> logger)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        _logger = logger;
    }

    public string ContentMineType => "application/json";

    public T Deserialize<T>(string? input)
    {
        return Deserialize<T>(input, new JsonSerializationConfiguration { });
    }

    public T Deserialize<T>(string? input, JsonSerializationConfiguration configuration)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                _logger.LogWarning($"Input value can't be empty or null.");
                throw new DeserializerException(Resources.JsonDeserializerValueCanNotBeEmpty);
            }

            var settings = new JsonSerializerSettings
            {
                Formatting = configuration.Indented ? Formatting.Indented : Formatting.None,
                ContractResolver = configuration.NameCaseInsensitive ? new DefaultContractResolver() : new CamelCasePropertyNamesContractResolver()
            };

            if (configuration.Converters is not null)
                settings.Converters = configuration.Converters.ConvertAll(item => (JsonConverter)item);
            
            return JsonConvert.DeserializeObject<T>(input, settings);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in deserialize data. Message: {ex.Message}");
            throw new DeserializerException(ex.Message);
        }
    }
}