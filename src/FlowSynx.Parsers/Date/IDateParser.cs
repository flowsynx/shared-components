namespace FlowSynx.Parsers.Date;

public interface IDateParser : IParser
{
    DateTime Parse(string dateTime);
}