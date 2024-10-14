using FlowSynx.IO.Compression;

namespace FlowSynx.Connectors.Abstractions;

public abstract class Connector
{
    public abstract Guid Id { get; }
    public abstract string Name { get; }
    public abstract Namespace Namespace { get; }
    public string Type => $"FlowSynx.{Namespace}/{Name}";
    public abstract string? Description { get; }
    public abstract Specifications? Specifications { get; set; }
    public abstract Type SpecificationsType { get; }

    public abstract Task Initialize();

    public abstract Task<object> About(Context context,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task CreateAsync(Context context, ConnectorOptions? options, 
        CancellationToken cancellationToken = default);

    public abstract Task WriteAsync(Context context, ConnectorOptions? options, 
        object dataOptions, CancellationToken cancellationToken = default);

    public abstract Task<ReadResult> ReadAsync(Context context, ConnectorOptions? options, 
        CancellationToken cancellationToken = default);

    public abstract Task UpdateAsync(Context context, ConnectorOptions? options, 
        CancellationToken cancellationToken = default);

    public abstract Task DeleteAsync(Context context, ConnectorOptions? options, 
        CancellationToken cancellationToken = default);

    public abstract Task<bool> ExistAsync(Context context, ConnectorOptions? options, 
        CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<object>> ListAsync(Context context, ConnectorOptions? options, 
        CancellationToken cancellationToken = default);

    public abstract Task TransferAsync(Context sourceContext, Connector? destinationConnector,
        Context destinationContext, ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task ProcessTransferAsync(Context sourceContext, TransferData transferData,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<CompressEntry>> CompressAsync(Context context,
        ConnectorOptions? options, CancellationToken cancellationToken = default);
}