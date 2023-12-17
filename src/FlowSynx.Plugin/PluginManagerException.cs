using FlowSynx.Abstractions.Exceptions;

namespace FlowSynx.Plugin;

public class PluginManagerException : FlowSynxException
{
    public PluginManagerException(string message) : base(message) { }
    public PluginManagerException(string message, Exception inner) : base(message, inner) { }
}