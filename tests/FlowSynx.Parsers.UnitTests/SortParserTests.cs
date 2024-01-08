using FlowSynx.Parsers.Sort;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlowSynx.Parsers.UnitTests;

public class SortParserTests : IDisposable
{
    private readonly ISortParser _sortParser;
    private readonly IEnumerable<string> _properties;

    public SortParserTests()
    {
        var loggerMock = new Mock<ILogger<SortParser>>();
        var logger = loggerMock.Object;

        _sortParser = new SortParser(logger);
        _properties = new List<string>() { "Id", "Name", "Date" };
    }

    public void Dispose()
    {
        _sortParser.Dispose();
    }

    [Theory]
    [MemberData(nameof(GetSortInfoFromDataGenerator))]
    public void GivenAStringAndPropertiesName_WhenSortParseIsCalled_ThenCorrectSortInfoIsReturned(string input, List<SortInfo> expected)
    {
        var index = 0;
        foreach (var sortInfo in _sortParser.Parse(input, _properties))
        {
            Assert.Equal(expected[index++], sortInfo);
        }
    }

    public static IEnumerable<object[]> GetSortInfoFromDataGenerator()
    {
        return new List<object[]>
        {
            new object[] { "Id asc", new List<SortInfo>
            {
                new SortInfo {Name = "Id", Direction = SortDirection.Ascending}
            }},

            new object[] { "Id asc, Name desc", new List<SortInfo> {
                new SortInfo { Name = "Id", Direction = SortDirection.Ascending },
                new SortInfo {Name = "Name", Direction = SortDirection.Descending}} },

            new object[] { "Date desc, Name asc", new List<SortInfo> {
                new SortInfo { Name = "Date", Direction = SortDirection.Descending },
                new SortInfo {Name = "Name", Direction = SortDirection.Ascending}} }
        };
    }
}