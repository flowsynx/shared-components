using Microsoft.Extensions.Logging;

namespace FlowSynx.Environment;

public class DefaultEndpoint : IEndpoint
{
    private readonly ILogger<DefaultEndpoint> _logger;
    private readonly IEnvironmentManager _environmentManager;

    public DefaultEndpoint(ILogger<DefaultEndpoint> logger,IEnvironmentManager environmentManager)
    {
        _logger = logger;
        _environmentManager = environmentManager;
    }

    public int GetDefaultHttpPort()
    {
        var flowSyncPort = _environmentManager.Get(EnvironmentVariables.FlowsynxHttpPort);
        var _ = int.TryParse(flowSyncPort, out var result);
        return result > 0 ? result : EnvironmentVariables.FlowsynxDefaultPort;
    }

    public string GetDefaultHttpEndpoint()
    {
        var port = GetDefaultHttpPort();
        return $"http://127.0.0.1:{port}";
    }
}