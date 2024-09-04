using EnsureThat;
using FlowSynx.IO.Compression;
using FlowSynx.Plugin.Abstractions;
using Microsoft.Extensions.Logging;

namespace FlowSynx.Plugin.Services;

public class PluginService: IPluginService
{
    private readonly ILogger<PluginService> _logger;

    public PluginService(ILogger<PluginService> logger)
    {
        EnsureArg.IsNotNull(logger, nameof(logger));
        _logger = logger;
    }

    public async Task<object> About(PluginInstance instance, PluginFilters? filters, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.About(filters, cancellationToken);
    }

    public async Task<object> CreateAsync(PluginInstance instance, PluginFilters? filters, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.CreateAsync(instance.Entity, filters, cancellationToken);
    }

    public async Task<object> WriteAsync(PluginInstance instance, PluginFilters? filters, 
        object dataOptions, CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.WriteAsync(instance.Entity, filters, dataOptions, cancellationToken);
    }

    public async Task<object> ReadAsync(PluginInstance instance, PluginFilters? filters, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.ReadAsync(instance.Entity, filters, cancellationToken);
    }

    public async Task<object> UpdateAsync(PluginInstance instance, PluginFilters? filters, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.UpdateAsync(instance.Entity, filters, cancellationToken);
    }

    public async Task<IEnumerable<object>> DeleteAsync(PluginInstance instance, PluginFilters? filters, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.DeleteAsync(instance.Entity, filters, cancellationToken);
    }

    public async Task<bool> ExistAsync(PluginInstance instance, PluginFilters? filters, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.ExistAsync(instance.Entity, filters, cancellationToken);
    }

    public async Task<IEnumerable<object>> ListAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.ListAsync(instance.Entity, filters, cancellationToken);
    }

    public async Task<IEnumerable<object>> CopyAsync(PluginInstance sourceInstance, PluginInstance destinationInstance,
        PluginFilters? filters, CancellationToken cancellationToken = default)
    {
        var result = await sourceInstance.Plugin.PrepareTransmissionData(sourceInstance.Entity,
            filters, cancellationToken);

        var transmissionData = result.ToList();
        foreach (var data in transmissionData)
        {
            var replace = data.Key.Replace(sourceInstance.Entity, destinationInstance.Entity);
            data.Key = replace;
        }

        return await destinationInstance.Plugin.TransmitDataAsync(destinationInstance.Entity, filters,
            transmissionData,
            cancellationToken);
    }

    public async Task<IEnumerable<object>> MoveAsync(PluginInstance sourceInstance, PluginInstance destinationInstance,
        PluginFilters? filters, CancellationToken cancellationToken = default)
    {
        var result = await sourceInstance.Plugin.PrepareTransmissionData(sourceInstance.Entity,
            filters, cancellationToken);

        var transmissionData = result.ToList();
        foreach (var data in transmissionData)
        {
            var replace = data.Key.Replace(sourceInstance.Entity, destinationInstance.Entity);
            data.Key = replace;
        }

        var response = await destinationInstance.Plugin.TransmitDataAsync(destinationInstance.Entity, filters, transmissionData,
            cancellationToken);

        await sourceInstance.Plugin.DeleteAsync(sourceInstance.Entity, filters, cancellationToken);

        return response;
    }
    
    public async Task<IEnumerable<CompressEntry>> CompressAsync(PluginInstance instance, PluginFilters? filters,
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.CompressAsync(instance.Entity, filters, cancellationToken);
    }
}