namespace FlowSynx.Connectors.Abstractions;

public class Specifications : Dictionary<string, string?>
{
    public Specifications() : base(StringComparer.OrdinalIgnoreCase)
    {

    }
}