using Microsoft.Extensions.Logging;

namespace FlowSynx.Environment;

public class DefaultEndpoint : IEndpoint
{
    private readonly ILogger<DefaultEndpoint> _logger;
    private readonly IEnvironmentManager _environmentManager;
    private const string VariableName = "FLOWSYNC_HTTP_PORT";
    private const int DefaultPort = 5860;

    public DefaultEndpoint(ILogger<DefaultEndpoint> logger,IEnvironmentManager environmentManager)
    {
        _logger = logger;
        _environmentManager = environmentManager;
    }

    public int GetDefaultHttpPort()
    {
        var flowSyncPort = _environmentManager.Get(VariableName);
        var parsedPort = int.TryParse(flowSyncPort, out var result);
        return result > 0 ? result : DefaultPort;
    }

    public string GetDefaultHttpEndpoint()
    {
        var port = GetDefaultHttpPort();
        return $"http://127.0.0.1:{port}";
    }
}