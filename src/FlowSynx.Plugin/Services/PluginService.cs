using EnsureThat;
using FlowSynx.IO.Compression;
using FlowSynx.Plugin.Abstractions;
using Microsoft.Extensions.Logging;
using System.Data;

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

    public async Task<object> CreateAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.CreateAsync(instance.Entity, options, cancellationToken);
    }

    public async Task<object> WriteAsync(PluginInstance instance, PluginOptions? options, 
        object dataOptions, CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.WriteAsync(instance.Entity, options, dataOptions, cancellationToken);
    }

    public async Task<object> ReadAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.ReadAsync(instance.Entity, options, cancellationToken);
    }

    public async Task<object> UpdateAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.UpdateAsync(instance.Entity, options, cancellationToken);
    }

    public async Task<IEnumerable<object>> DeleteAsync(PluginInstance instance, PluginOptions? options, 
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.DeleteAsync(instance.Entity, options, cancellationToken);
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

    public async Task<IEnumerable<object>> CopyAsync(PluginInstance sourceInstance, PluginInstance destinationInstance,
        PluginOptions? options, CancellationToken cancellationToken = default)
    {
        var transmissionData = await sourceInstance.Plugin.PrepareTransmissionData(sourceInstance.Entity,
            options, cancellationToken);

        //if (transmissionData != null && transmissionData.Namespace == PluginNamespace.Storage)
        //{
        //    foreach (DataRow row in transmissionData.Data.Rows)
        //    {
        //        var replace = row["FullPath"].ToString().Replace(sourceInstance.Entity, destinationInstance.Entity);
        //        data.Key = replace;
        //    }
        //}
        return await destinationInstance.Plugin.TransmitDataAsync(destinationInstance.Entity, options,
            transmissionData, cancellationToken);
    }

    public async Task<IEnumerable<object>> MoveAsync(PluginInstance sourceInstance, PluginInstance destinationInstance,
        PluginOptions? options, CancellationToken cancellationToken = default)
    {
        var transmissionData = await sourceInstance.Plugin.PrepareTransmissionData(sourceInstance.Entity,
            options, cancellationToken);

        //var transmissionData = result.ToList();
        //foreach (var data in transmissionData)
        //{
        //    var replace = data.Key.Replace(sourceInstance.Entity, destinationInstance.Entity);
        //    data.Key = replace;
        //}

        var response = await destinationInstance.Plugin.TransmitDataAsync(destinationInstance.Entity, options, 
            transmissionData, cancellationToken);

        await sourceInstance.Plugin.DeleteAsync(sourceInstance.Entity, options, cancellationToken);

        return response;
    }
    
    public async Task<IEnumerable<CompressEntry>> CompressAsync(PluginInstance instance, PluginOptions? options,
        CancellationToken cancellationToken = default)
    {
        return await instance.Plugin.CompressAsync(instance.Entity, options, cancellationToken);
    }
}