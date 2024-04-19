using Microsoft.Extensions.Logging;

namespace FlowSynx.Environment;

public class DefaultEndpoint : IEndpoint
{
    private readonly ILogger<DefaultEndpoint> _logger;

    public DefaultEndpoint(ILogger<DefaultEndpoint> logger)
    {
        _logger = logger;
    }
    
    public int FlowSynxHttpPort() => EnvironmentVariables.FlowSynxHttpPort;
    public int FlowSynxDashboardHttpPort() => EnvironmentVariables.FlowSynxDashboardHttpPort;
    public string FlowSynxHttpEndpoint() => $"http://localhost:{FlowSynxHttpPort()}";
    public string FlowSynxDashboardHttpEndpoint() => $"http://localhost:{FlowSynxDashboardHttpPort()}";
}