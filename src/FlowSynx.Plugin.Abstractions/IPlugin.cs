namespace FlowSynx.Plugin.Abstractions;

public interface IPlugin: IDisposable
{
    Task Initialize();

    Guid Id { get; }

    string Name { get; }

    PluginNamespace Namespace { get; }

    string Type => $"FlowSynx.{Namespace}/{Name}";

    string? Description { get; }

    Dictionary<string, string?>? Specifications { get; set; }

    Type SpecificationsType { get; }

    Task<object> About(string entity, CancellationToken cancellationToken = default);

    Task<object> CreateAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<object> WriteAsync(string entity, PluginOptions? options, object dataOptions,
        CancellationToken cancellationToken = default);

    Task<object> ReadAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<object> UpdateAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> DeleteAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<object>> ListAsync(string entity, PluginOptions? options,
        CancellationToken cancellationToken = default);
}