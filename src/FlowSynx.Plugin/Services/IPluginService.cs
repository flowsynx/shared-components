using FlowSynx.IO.Compression;
using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Plugin.Services;

public interface IPluginService
{
    Task<object> About(PluginContext context, PluginOptions? options, 
        CancellationToken cancellationToken = default);

    Task CreateAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task WriteAsync(PluginContext context, PluginOptions? options, 
        object dataOptions, CancellationToken cancellationToken = default);

    Task<object> ReadAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> ListAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task TransferAsync(PluginContext sourceContenx, PluginContext destinationContenx,
        PluginOptions? options, CancellationToken cancellationToken = default);

    Task<IEnumerable<CompressEntry>> CompressAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default);
}