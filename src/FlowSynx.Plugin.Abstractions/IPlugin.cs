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

    Task<object> About(PluginFilters? filters, 
        CancellationToken cancellationToken = default);

    Task<object> CreateAsync(string entity, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<object> WriteAsync(string entity, PluginFilters? filters, object dataOptions,
        CancellationToken cancellationToken = default);

    Task<object> ReadAsync(string entity, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<object> UpdateAsync(string entity, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> DeleteAsync(string entity, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(string entity, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> ListAsync(string entity, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TransmissionData>> PrepareCopyAsync(string entity, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<object> CopyAsync(string entity, PluginFilters? filters, 
        IEnumerable<TransmissionData> transmissionData, CancellationToken cancellationToken = default);

    Task<IEnumerable<CompressEntry>> CompressAsync(string entity, PluginFilters? filters,
        CancellationToken cancellationToken = default);
}