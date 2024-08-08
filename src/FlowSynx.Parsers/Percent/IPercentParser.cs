namespace FlowSynx.Parsers.Percent;

public interface IPercentParser : IParser
{
    int Parse(string? value, long total);
}