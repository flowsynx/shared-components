using Microsoft.Extensions.Logging;

namespace FlowSynx.Environment;

public class DefaultEndpoint : IEndpoint
{
    private readonly ILogger<DefaultEndpoint> _logger;

    public DefaultEndpoint(ILogger<DefaultEndpoint> logger)
    {
        _logger = logger;
    }

    public int GetDefaultHttpPort() => EnvironmentVariables.FlowsynxDefaultPort;
    public string GetDefaultHttpEndpoint() => $"http://localhost:{GetDefaultHttpPort()}";
}