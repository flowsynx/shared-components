using FlowSynx.Environment;
using FlowSynx.Parsers.Date;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlowSynx.Parsers.UnitTests;

public class DateParserTests : IDisposable
{
    private readonly IDateParser _dateParser;

    public DateParserTests()
    {
        var loggerMock = new Mock<ILogger<DateParser>>();
        var logger = loggerMock.Object;

        var systemClockMock = new Mock<ISystemClock>();
        var systemClock = systemClockMock.Object;

        systemClockMock
            .Setup(c => c.NowUtc)
            .Returns(new DateTime(2024, 01, 01, 0, 0, 0, 0));

        _dateParser = new DateParser(logger, systemClock);
    }

    public void Dispose()
    {

        _dateParser.Dispose();
    }
    
    [Theory]
    [InlineData("03/04/2024 08:44:34 PM", "03/04/2024 08:44:34 PM")]
    [InlineData("10/25/2024 12:05:01 AM", "10/25/2024 12:05:01 AM")]
    [InlineData("12/30/2024 00:00:00", "12/30/2024 00:00:00")]
    [InlineData("0", "01/01/2024 00:00:00")]                            //Adding 0      Seconds
    [InlineData("10", "01/01/2024 00:00:10")]                           //Adding 10     Seconds
    [InlineData("0y", "01/01/2024 00:00:00")]                           //Adding 0      Year
    [InlineData("0.5y", "07/02/2024 00:00:00")]                         //Adding 0.5    Year
    [InlineData("1M", "02/01/2024 00:00:00")]                           //Adding 1      Month
    [InlineData("1y1M", "02/01/2025 00:00:00")]                         //Adding 1      Year and 1 Month
    [InlineData("1w", "01/08/2024 00:00:00")]                           //Adding 1      Week
    [InlineData("3M1w", "04/08/2024 00:00:00")]                         //Adding 3      Months and 1 week
    [InlineData("1y3M1w", "04/08/2025 00:00:00")]                       //Adding 1      Year and 3 Months and 1 week
    [InlineData("3d", "01/04/2024 00:00:00")]                           //Adding 3      Days
    [InlineData("3.5d", "01/04/2024 12:00:00")]                         //Adding 3.5    Days
    [InlineData("1y3.5d", "01/04/2025 12:00:00")]                       //Adding 1      Year and 3.5 days
    [InlineData("3h", "01/01/2024 03:00:00")]                           //Adding 3      Hours
    [InlineData("3.5h", "01/01/2024 03:30:00")]                         //Adding 3.5    Hours
    [InlineData("1w3.5h", "01/08/2024 03:30:00")]                       //Adding 1      Week and 3.5 hours
    [InlineData("3m", "01/01/2024 00:03:00")]                           //Adding 3      Minutes
    [InlineData("18s", "01/01/2024 00:00:18")]                          //Adding 18     Seconds
    [InlineData("18ms", "01/01/2024 00:00:00.018")]                     //Adding 18     Milliseconds
    [InlineData("10m20s30ms", "01/01/2024 00:10:20.030")]               //Adding 10     Minutes, 20 Seconds, and 30 Milliseconds
    [InlineData("27m16ms", "01/01/2024 00:27:00.016")]                  //Adding 27     Minutes, and 16 Milliseconds
    public void GivenAString_WhenDateParseIsCalled_ThenDateTimeIsReturned(string input, string dateTime)
    {
        var expected = DateTime.Parse(dateTime);
        var actual = _dateParser.Parse(input);
        Assert.Equal(expected, actual);
    }
}