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

    public async Task<object> About(PluginContex contenx, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await contenx.Plugin.About(options, cancellationToken);
    }

    public async Task CreateAsync(PluginContex contenx, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await contenx.Plugin.CreateAsync(contenx.Entity, options, cancellationToken);
    }

    public async Task WriteAsync(PluginContex contenx, PluginOptions? options, 
        object dataOptions, CancellationToken cancellationToken = default)
    {
        await contenx.Plugin.WriteAsync(contenx.Entity, options, dataOptions, cancellationToken);
    }

    public async Task<object> ReadAsync(PluginContex contenx, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await contenx.Plugin.ReadAsync(contenx.Entity, options, cancellationToken);
    }

    public async Task UpdateAsync(PluginContex contenx, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await contenx.Plugin.UpdateAsync(contenx.Entity, options, cancellationToken);
    }

    public async Task DeleteAsync(PluginContex contenx, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await contenx.Plugin.DeleteAsync(contenx.Entity, options, cancellationToken);
    }

    public async Task<bool> ExistAsync(PluginContex contenx, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await contenx.Plugin.ExistAsync(contenx.Entity, options, cancellationToken);
    }

    public async Task<IEnumerable<object>> ListAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default)
    {
        return await contenx.Plugin.ListAsync(contenx.Entity, options, cancellationToken);
    }

    public async Task TransferAsync(PluginContex sourceContenx, PluginContex destinationContenx,
        PluginOptions? options, CancellationToken cancellationToken = default)
    {
        var transmissionData = await sourceContenx.Plugin.PrepareTransferring(sourceContenx.Entity,
            options, cancellationToken);

        if (transmissionData is { PluginNamespace: PluginNamespace.Storage })
        {
            foreach (var row in transmissionData.Rows)
            {
                var replace = row.Key.Replace(sourceContenx.Entity, destinationContenx.Entity);
                row.Key = replace;
            }
        }

        await destinationContenx.Plugin.TransferAsync(destinationContenx.Entity, options,
            transmissionData, cancellationToken);

        if (transmissionData.Kind == TransferKind.Move)
            await sourceContenx.Plugin.DeleteAsync(sourceContenx.Entity, options, cancellationToken);
    }
    
    public async Task<IEnumerable<CompressEntry>> CompressAsync(PluginContex contenx, PluginOptions? options,
        CancellationToken cancellationToken = default)
    {
        return await contenx.Plugin.CompressAsync(contenx.Entity, options, cancellationToken);
    }
}