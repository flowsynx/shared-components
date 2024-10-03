using FlowSynx.IO.Compression;
using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Plugin.Services;

public interface IPluginService
{
    Task<object> About(PluginContex contenx, PluginOptions? options, 
        CancellationToken cancellationToken = default);

    Task CreateAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task WriteAsync(PluginContex contenx, PluginOptions? options, 
        object dataOptions, CancellationToken cancellationToken = default);

    Task<object> ReadAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> ListAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task TransferAsync(PluginContex sourceContenx, PluginContex destinationContenx,
        PluginOptions? options, CancellationToken cancellationToken = default);

    Task<IEnumerable<CompressEntry>> CompressAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default);
}