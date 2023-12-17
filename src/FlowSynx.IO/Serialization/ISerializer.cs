namespace FlowSynx.IO.Serialization;

public interface ISerializer
{
    string ContentMineType { get; }
    string Serialize(object? input);
    string Serialize(object? input, JsonSerializationConfiguration configuration);
}