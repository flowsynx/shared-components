namespace FlowSynx.Environment;

public interface IEndpoint
{
    int FlowSynxHttpPort();
    int FlowSynxDashboardHttpPort();
    string FlowSynxHttpEndpoint();
    string FlowSynxDashboardHttpEndpoint();
}