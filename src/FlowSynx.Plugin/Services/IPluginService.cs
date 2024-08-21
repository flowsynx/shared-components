using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Plugin.Services;

public interface IPluginService
{
    Task<object> About(PluginInstance instance, PluginFilters? filters, 
        CancellationToken cancellationToken = default);

    Task<object> CreateAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<object> WriteAsync(PluginInstance instance, PluginFilters? filters, 
        object dataOptions, CancellationToken cancellationToken = default);

    Task<object> ReadAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<object> UpdateAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> DeleteAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> ListAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> CopyAsync(PluginInstance sourceInstance, PluginFilters? sourceFilters,
        PluginInstance destinationInstance, PluginFilters? destinationFilters, 
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> MoveAsync(PluginInstance sourceInstance, PluginFilters? sourceFilters, 
        PluginInstance destinationInstance, PluginFilters? destinationFilters, 
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CheckResult>> CheckAsync(PluginInstance sourceInstance, PluginInstance destinationInstance,
        PluginFilters? filters, CancellationToken cancellationToken = default);

    Task<CompressResult> CompressAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default);
}