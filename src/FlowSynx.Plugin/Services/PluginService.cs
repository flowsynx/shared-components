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

    public async Task<object> About(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.About(options, cancellationToken);
    }

    public async Task CreateAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await instance.Plugin.CreateAsync(instance.Entity, options, cancellationToken);
    }

    public async Task WriteAsync(PluginInstance instance, PluginOptions? options, 
        object dataOptions, CancellationToken cancellationToken = default)
    {
        await instance.Plugin.WriteAsync(instance.Entity, options, dataOptions, cancellationToken);
    }

    public async Task<object> ReadAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.ReadAsync(instance.Entity, options, cancellationToken);
    }

    public async Task UpdateAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await instance.Plugin.UpdateAsync(instance.Entity, options, cancellationToken);
    }

    public async Task DeleteAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await instance.Plugin.DeleteAsync(instance.Entity, options, cancellationToken);
    }

    public async Task<bool> ExistAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.ExistAsync(instance.Entity, options, cancellationToken);
    }

    public async Task<IEnumerable<object>> ListAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.ListAsync(instance.Entity, options, cancellationToken);
    }

    public async Task TransferAsync(PluginInstance sourceInstance, PluginInstance destinationInstance,
        PluginOptions? options, CancellationToken cancellationToken = default)
    {
        var transmissionData = await sourceInstance.Plugin.PrepareTransferring(sourceInstance.Entity,
            options, cancellationToken);

        if (transmissionData is { PluginNamespace: PluginNamespace.Storage })
        {
            foreach (var row in transmissionData.Rows)
            {
                var replace = row.Key.Replace(sourceInstance.Entity, destinationInstance.Entity);
                row.Key = replace;
            }
        }

        await destinationInstance.Plugin.TransferAsync(destinationInstance.Entity, options,
            transmissionData, cancellationToken);

        if (transmissionData.State == TransferState.Move)
            await sourceInstance.Plugin.DeleteAsync(sourceInstance.Entity, options, cancellationToken);
    }
    
    public async Task<IEnumerable<CompressEntry>> CompressAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.CompressAsync(instance.Entity, options, cancellationToken);
    }
}