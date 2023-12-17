namespace FlowSynx.Parsers.Size;

public interface ISizeParser : IParser
{
    long Parse(string size);
}