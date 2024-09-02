namespace FlowSynx.Plugin.Abstractions;

public class TransmissionData
{
    public string Key { get; set; }
    public object? Content { get; set; }
    public string? ContentType { get; set; }

    public TransmissionData(string key)
    {
        Key = key;
    }

    public TransmissionData(string key, object content)
    {
        Key = key;
        Content = content;
    }

    public TransmissionData(string key, object content, string? contentType)
    {
        Key = key;
        Content = content;
        ContentType = contentType;
    }
}