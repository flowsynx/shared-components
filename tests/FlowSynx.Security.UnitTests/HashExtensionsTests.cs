namespace FlowSynx.Security.UnitTests;

public class HashExtensionsTests
{
    [Theory]
    [InlineData("TestFile", "E1F53BF8B56F5DF417E0039934F5B7C2")]
    [InlineData("FlowSynx.exe", "7F3A8BD187E6014E1D5269CB22753BDB")]
    [InlineData("Program Files", "0D4F03E079D7F47617746E2CA64C1469")]
    public void GivenAString_WhenCreateMd5IsCalled_ThenMd5IsReturned(string input, string expected)
    {
        Assert.Equal(expected, HashHelper.Md5.GetHash(input));
    }
}