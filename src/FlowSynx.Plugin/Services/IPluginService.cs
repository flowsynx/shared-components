using FlowSynx.IO.Compression;
using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Plugin.Services;

public interface IPluginService
{
    Task<object> About(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default);

    Task<object> CreateAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<object> WriteAsync(PluginInstance instance, PluginOptions? options, 
        object dataOptions, CancellationToken cancellationToken = default);

    Task<object> ReadAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<object> UpdateAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> DeleteAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> ListAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> CopyAsync(PluginInstance sourceInstance, PluginInstance destinationInstance,
        PluginOptions? options, CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> MoveAsync(PluginInstance sourceInstance, PluginInstance destinationInstance,
        PluginOptions? options, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<CompressEntry>> CompressAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default);
}