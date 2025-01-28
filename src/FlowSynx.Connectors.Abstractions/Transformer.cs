namespace FlowSynx.Connectors.Abstractions;

public abstract class Transformer
{
    public abstract Guid Id { get; }
    public abstract string Name { get; }
    public string Type => $"FlowSynx.Transformers/{Name}";
    public abstract string? Description { get; }
    public abstract Specifications? Specifications { get; set; }
    public abstract Type SpecificationsType { get; }

    public abstract Task Initialize();
}