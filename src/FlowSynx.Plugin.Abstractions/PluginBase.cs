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

    public abstract Task<object> About(PluginBase? inferiorPlugin, 
        PluginOptions? options, CancellationToken cancellationToken = default);

    public abstract Task CreateAsync(string entity, PluginBase? inferiorPlugin,
        PluginOptions? options, CancellationToken cancellationToken = default);

    public abstract Task WriteAsync(string entity, PluginBase? inferiorPlugin,
        PluginOptions? options, object dataOptions, 
        CancellationToken cancellationToken = default);

    public abstract Task<ReadResult> ReadAsync(string entity, PluginBase? inferiorPlugin,
        PluginOptions? options, CancellationToken cancellationToken = default);

    public abstract Task UpdateAsync(string entity, PluginBase? inferiorPlugin,
        PluginOptions? options, CancellationToken cancellationToken = default);

    public abstract Task DeleteAsync(string entity, PluginBase? inferiorPlugin, 
        PluginOptions? options, CancellationToken cancellationToken = default);

    public abstract Task<bool> ExistAsync(string entity, PluginBase? inferiorPlugin,
        PluginOptions? options, CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<object>> ListAsync(string entity, PluginBase? inferiorPlugin, 
        PluginOptions? options, CancellationToken cancellationToken = default);

    public abstract Task<TransferData> PrepareTransferring(string entity, PluginBase? inferiorPlugin, 
        PluginOptions? options, CancellationToken cancellationToken = default);

    public abstract Task TransferAsync(string entity, PluginBase? inferiorPlugin,
        PluginOptions? options, TransferData transferData, CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<CompressEntry>> CompressAsync(string entity, PluginBase? inferiorPlugin, 
        PluginOptions? options, CancellationToken cancellationToken = default);
}