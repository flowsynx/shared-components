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

    public abstract Task<object> About(Context context, CancellationToken cancellationToken = default);

    public abstract Task CreateAsync(Context context, CancellationToken cancellationToken = default);

    public abstract Task WriteAsync(Context context, CancellationToken cancellationToken = default);

    public abstract Task<ReadResult> ReadAsync(Context context, CancellationToken cancellationToken = default);

    public abstract Task UpdateAsync(Context context, CancellationToken cancellationToken = default);

    public abstract Task DeleteAsync(Context context, CancellationToken cancellationToken = default);

    public abstract Task<bool> ExistAsync(Context context, CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<object>> ListAsync(Context context, CancellationToken cancellationToken = default);

    public abstract Task TransferAsync(Context sourceContext, Context destinationContext, TransferKind transferKind, 
        CancellationToken cancellationToken = default);

    public abstract Task ProcessTransferAsync(Context context, TransferData transferData, TransferKind transferKind, 
        CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<CompressEntry>> CompressAsync(Context context, CancellationToken cancellationToken = default);
}