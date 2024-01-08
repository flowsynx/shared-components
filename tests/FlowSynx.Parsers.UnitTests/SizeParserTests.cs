using FlowSynx.Environment;
using FlowSynx.Parsers.Size;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlowSynx.Parsers.UnitTests;

public class SizeParserTests : IDisposable
{
    private readonly ISizeParser _sizeParser;

    public SizeParserTests()
    {
        var loggerMock = new Mock<ILogger<SizeParser>>();
        var logger = loggerMock.Object;

        _sizeParser = new SizeParser(logger);
    }

    public void Dispose()
    {
        _sizeParser.Dispose();
    }
    
    [Theory]
    [InlineData("1K", 1024)]
    [InlineData("10K", 10240)]
    [InlineData("13K", 13312)]
    [InlineData("63K", 64512)]
    [InlineData("1M", 1048576)]
    [InlineData("6M", 6291456)]
    [InlineData("128M", 134217728)]
    [InlineData("1G", 1073741824)]
    [InlineData("48G", 51539607552)]
    [InlineData("1T", 1099511627776)]
    [InlineData("5T", 5497558138880)]
    [InlineData("1P", 1125899906842624)]
    [InlineData("11P", 12384898975268864)]
    public void GivenAString_WhenSizeParseIsCalled_ThenCorrectSizeIsReturned(string input, long expected)
    {
        var actual = _sizeParser.Parse(input);
        Assert.Equal(expected, actual);
    }
}