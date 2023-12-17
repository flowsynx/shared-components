using EnsureThat;
using FlowSynx.IO.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlowSynx.IO.Serialization;

public class JsonSerializer : ISerializer
{
    private readonly ILogger<JsonSerializer> _logger;

    public JsonSerializer(ILogger<JsonSerializer> logger)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        _logger = logger;
    }

    public string ContentMineType => "application/json";

    public string Serialize(object? input)
    {
        return Serialize(input, new JsonSerializationConfiguration { Indented = false });
    }

    public string Serialize(object? input, JsonSerializationConfiguration configuration)
    {
        try
        {
            if (input is null)
            {
                _logger.LogWarning($"Input value can't be empty or null.");
                throw new SerializerException(Resources.JsonSerializerValueCanNotBeEmpty);
            }

            var settings = new JsonSerializerSettings
            {
                Formatting = configuration.Indented ? Formatting.Indented : Formatting.None,
                ContractResolver = configuration.NameCaseInsensitive ? new DefaultContractResolver() : new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(input, settings);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in serializer data. Message: {ex.Message}");
            throw new SerializerException(ex.Message);
        }
    }
}