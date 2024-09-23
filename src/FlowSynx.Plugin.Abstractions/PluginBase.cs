using FlowSynx.IO.Compression;

namespace FlowSynx.Plugin.Abstractions;

public abstract class PluginBase
{
    public abstract Guid Id { get; }
    public abstract string Name { get; }
    public abstract PluginNamespace Namespace { get; }
    public string Type => $"FlowSynx.{Namespace}/{Name}";
    public abstract string? Description { get; }
    public abstract PluginSpecifications? Specifications { get; set; }
    public abstract Type SpecificationsType { get; }

    public abstract Task Initialize();

    public abstract Task<object> About(PluginOptions? options, 
        CancellationToken cancellationToken = default);

    public abstract Task CreateAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    public abstract Task WriteAsync(string entity, PluginOptions? options, object dataOptions,
        CancellationToken cancellationToken = default);

    public abstract Task<object> ReadAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    public abstract Task UpdateAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    public abstract Task DeleteAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    public abstract Task<bool> ExistAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<object>> ListAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    public abstract Task<TransmissionData> PrepareTransmissionData(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    public abstract Task TransmitDataAsync(string entity, PluginOptions? options, 
        TransmissionData transmissionData, CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<CompressEntry>> CompressAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);
}