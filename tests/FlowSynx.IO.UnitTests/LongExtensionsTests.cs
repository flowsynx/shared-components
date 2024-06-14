namespace FlowSynx.IO.UnitTests;

public class LongExtensionsTests
{
    [Theory]
    [InlineData(1, "1 B")]
    [InlineData(1024, "1 KiB")]
    [InlineData(2036, "1.99 KiB")]
    [InlineData(1048576, "1 MiB")]
    [InlineData(5780000, "5.51 MiB")]
    [InlineData(1073741824, "1 GiB")]
    [InlineData(1551859712, "1.45 GiB")]
    [InlineData(1099511627776, "1 TiB")]
    [InlineData(1125899906842624, "1 PiB")]
    [InlineData(1152921504606846976, "1 EiB")]
    public void GivenALong_WhenToStringIsCalledAndApplyFormat_ThenCorrectByteFormatIsReturned(long input, string expected)
    {
        Assert.Equal(expected, input.ToString(true));
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(1024, "1024")]
    [InlineData(2036, "2036")]
    [InlineData(1048576, "1048576")]
    [InlineData(5780000, "5780000")]
    [InlineData(1073741824, "1073741824")]
    [InlineData(1551859712, "1551859712")]
    [InlineData(1099511627776, "1099511627776")]
    [InlineData(1125899906842624, "1125899906842624")]
    [InlineData(1152921504606846976, "1152921504606846976")]
    public void GivenALong_WhenToStringIsCalledAndDoesNotApplyFormat_ThenCorrectByteFormatIsReturned(long input, string expected)
    {
        Assert.Equal(expected, input.ToString(false));
    }
}