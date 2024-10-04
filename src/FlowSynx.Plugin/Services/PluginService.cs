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

    public async Task<object> About(PluginContext context, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await context.Plugin.About(options, cancellationToken);
    }

    public async Task CreateAsync(PluginContext context, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await context.Plugin.CreateAsync(context.Entity, options, cancellationToken);
    }

    public async Task WriteAsync(PluginContext context, PluginOptions? options, 
        object dataOptions, CancellationToken cancellationToken = default)
    {
        await context.Plugin.WriteAsync(context.Entity, options, dataOptions, cancellationToken);
    }

    public async Task<object> ReadAsync(PluginContext context, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await context.Plugin.ReadAsync(context.Entity, options, cancellationToken);
    }

    public async Task UpdateAsync(PluginContext context, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await context.Plugin.UpdateAsync(context.Entity, options, cancellationToken);
    }

    public async Task DeleteAsync(PluginContext context, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        await context.Plugin.DeleteAsync(context.Entity, options, cancellationToken);
    }

    public async Task<bool> ExistAsync(PluginContext context, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await context.Plugin.ExistAsync(context.Entity, options, cancellationToken);
    }

    public async Task<IEnumerable<object>> ListAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default)
    {
        return await context.Plugin.ListAsync(context.Entity, options, cancellationToken);
    }

    public async Task TransferAsync(PluginContext sourceContenx, PluginContext destinationContenx,
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
    
    public async Task<IEnumerable<CompressEntry>> CompressAsync(PluginContext context, PluginOptions? options,
        CancellationToken cancellationToken = default)
    {
        return await context.Plugin.CompressAsync(context.Entity, options, cancellationToken);
    }
}