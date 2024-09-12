using FlowSynx.IO.Compression;

namespace FlowSynx.Plugin.Abstractions;

public interface IPlugin: IDisposable
{
    Task Initialize();

    Guid Id { get; }

    string Name { get; }

    PluginNamespace Namespace { get; }

    string Type => $"FlowSynx.{Namespace}/{Name}";

    string? Description { get; }

    PluginSpecifications? Specifications { get; set; }

    Type SpecificationsType { get; }

    Task<object> About(PluginOptions? options, 
        CancellationToken cancellationToken = default);

    Task<object> CreateAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<object> WriteAsync(string entity, PluginOptions? options, object dataOptions,
        CancellationToken cancellationToken = default);

    Task<object> ReadAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<object> UpdateAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> DeleteAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> ListAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TransmissionData>> PrepareTransmissionData(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> TransmitDataAsync(string entity, PluginOptions? options, 
        IEnumerable<TransmissionData> transmissionData, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<CompressEntry>> CompressAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);
}