namespace FlowSynx.IO.UnitTests;

public class PathHelperTests
{
    [Theory]
    [InlineData("dev/one", new[] { "dev", "one" })]
    [InlineData("one/two/three", new[] { "one", "two", "three" })]
    [InlineData("three", new[] { "one", "..", "three" })]
    public void GivenStringArray_WhenCombineIsCalled_ThenCorrectCombinedPathIsReturned(string expected, string[] parts)
    {
        Assert.Equal(expected, PathHelper.Combine(parts));
    }

    [Theory]
    [InlineData("", "/dev/..")]
    [InlineData("storage", "/dev/../storage")]
    [InlineData("one", "/one")]
    [InlineData("one", "/one/")]
    [InlineData("", "/one/../../../../")]
    public void GivenStringArray_WhenNormalizeIsCalled_ThenCorrectNormalizePathIsReturned(string expected, string path)
    {
        Assert.Equal(expected, PathHelper.Normalize(path));
    }

    [Theory]
    [InlineData("dev", "/dev/")]
    [InlineData("dev/storage", "/dev/storage/")]
    [InlineData("one", "/one")]
    [InlineData("one", "/one/")]
    public void GivenStringArray_WhenNormalizePartIsCalled_ThenCorrectNormalizePartIsReturned(string expected, string path)
    {
        Assert.Equal(expected, PathHelper.NormalizePart(path));
    }

    [Theory]
    [InlineData(new[] {"one", "two"}, "one/two/")]
    [InlineData(new[] { "one", "two", "three" }, "one/two/three")]
    public void GivenStringArray_WhenSplitIsCalled_ThenCorrectSplitPathIsReturned(string[] expected, string path)
    {
        Assert.Equal(expected, PathHelper.Split(path));
    }

    [Theory]
    [InlineData("one/two/", "one/two/three")]
    [InlineData("one/", "one/two")]
    [InlineData("two/", "one/../two/three")]
    [InlineData("two/", "one/../two/three/four/..")]
    public void GivenStringArray_WhenGetParentIsCalled_ThenCorrectParentPathIsReturned(string expected, string path)
    {
        Assert.Equal(expected, PathHelper.GetParent(path));
    }

    [Theory]
    [InlineData(true, "/")]
    [InlineData(true, "")]
    [InlineData(false, "/two/three")]
    [InlineData(false, "/four/..")]
    public void GivenStringArray_WhenIsRootPathIsCalled_ThenCorrectAnswerReturned(bool expected, string path)
    {
        Assert.Equal(expected, PathHelper.IsRootPath(path));
    }

    [Theory]
    [InlineData("/", "\\")]
    [InlineData("/one/", "\\one\\")]
    [InlineData("/one/two/", "\\one\\two\\")]
    public void GivenStringArray_WhenToUnixPathIsCalled_ThenCorrectAnswerReturned(string expected, string path)
    {
        Assert.Equal(expected, PathHelper.ToUnixPath(path));
    }
}