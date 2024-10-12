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

    public abstract Task<object> About(Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task CreateAsync(string entity, Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task WriteAsync(string entity, Connector? connector,
        ConnectorOptions? options, object dataOptions, 
        CancellationToken cancellationToken = default);

    public abstract Task<ReadResult> ReadAsync(string entity, Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task UpdateAsync(string entity, Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task DeleteAsync(string entity, Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task<bool> ExistAsync(string entity, Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<object>> ListAsync(string entity, Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task<TransferData> PrepareTransferring(string entity, Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);

    public abstract Task TransferAsync(string entity, Connector? connector,
        ConnectorOptions? options, TransferData transferData, CancellationToken cancellationToken = default);

    public abstract Task<IEnumerable<CompressEntry>> CompressAsync(string entity, Connector? connector,
        ConnectorOptions? options, CancellationToken cancellationToken = default);
}