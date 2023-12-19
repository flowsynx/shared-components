namespace FlowSynx.Environment;

public interface IEnvironmentManager
{
    string? Get(string variableName);
    void Set(string variableName, string value);
}