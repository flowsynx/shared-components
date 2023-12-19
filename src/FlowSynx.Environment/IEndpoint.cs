namespace FlowSynx.Environment;

public interface IEndpoint
{
    int GetDefaultHttpPort();
    string GetDefaultHttpEndpoint();
}