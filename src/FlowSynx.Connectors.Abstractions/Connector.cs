using FlowSynx.Data;
using FlowSynx.IO.Compression;

namespace FlowSynx.Connectors.Abstractions;

public abstract class Connector
{
    public abstract Guid Id { get; }
    public abstract string Name { get; }
    public abstract Namespace Namespace { get; }
    public string Type => $"FlowSynx.{Namespace}/{Name}";
    public abstract string? Description { get; }
    public abstract Specifications? Specifications { get; set; }
    public abstract Type SpecificationsType { get; }
    public abstract Task Initialize();
}