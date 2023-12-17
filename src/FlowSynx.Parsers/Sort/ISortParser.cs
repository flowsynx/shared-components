namespace FlowSynx.Parsers.Sort;

public interface ISortParser : IParser
{
    List<SortInfo> Parse(string sortStatement, IEnumerable<string> properties);
}