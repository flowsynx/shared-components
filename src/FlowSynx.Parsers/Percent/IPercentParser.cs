using FlowSynx.Parsers;
using FlowSynx.Plugin.Abstractions;

namespace FlowSynx.Parsers.Percent;

public interface IPercentParser : IParser
{
    int Parse(string? value, long total);
}