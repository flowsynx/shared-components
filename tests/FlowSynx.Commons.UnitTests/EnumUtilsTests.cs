namespace FlowSynx.Commons.UnitTests;

public class EnumUtilsTests
{
    public enum TestEnum
    {
        Test1,
        Test2, 
        Test3
    }

    [Theory]
    [InlineData("Test1", true, TestEnum.Test1)]
    [InlineData("Test2", true, TestEnum.Test2)]
    [InlineData("Test3", true, TestEnum.Test3)]
    [InlineData("Test4", false, TestEnum.Test1)]
    public void GivenAString_WhenTryParseWithMemberNameIsCalled_ThenCorrectResultIsReturned(string input, bool result, TestEnum expected)
    {
        var actual = EnumUtils.TryParseWithMemberName<TestEnum>(input, out var enumResult);
        Assert.Equal(actual, result);
        Assert.Equal(expected, enumResult);
    }

    [Theory]
    [InlineData("Test1", TestEnum.Test1)]
    [InlineData("Test2", TestEnum.Test2)]
    [InlineData("Test3", TestEnum.Test3)]
    public void GivenAString_WhenGetEnumValueOrDefaultIsCalled_ThenCorrectEnumIsReturned(string input, TestEnum expected)
    {
        var actual = EnumUtils.GetEnumValueOrDefault<TestEnum>(input)!.Value;
        Assert.Equal(expected, actual);
    }
}