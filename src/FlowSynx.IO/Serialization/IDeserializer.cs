namespace FlowSynx.IO.Serialization;

public interface IDeserializer
{
    string ContentMineType { get; }
    T Deserialize<T>(string? input);
    T Deserialize<T>(string input, JsonSerializationConfiguration configuration);
}