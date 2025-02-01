namespace FlowSynx.Connectors.Abstractions;

public class ConnectorMetadataAttribute: Attribute
{
    public LinkType LinkType { get; }

    public ConnectorMetadataAttribute(LinkType linkType)
    {
        LinkType = linkType;
    }
}