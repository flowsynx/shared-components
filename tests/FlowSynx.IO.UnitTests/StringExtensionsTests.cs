namespace FlowSynx.IO.UnitTests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("FlowSynx", "Rmxvd1N5bng=")]

    [InlineData("Syncs and manage your files to different storage", 
        "U3luY3MgYW5kIG1hbmFnZSB5b3VyIGZpbGVzIHRvIGRpZmZlcmVudCBzdG9yYWdl")]

    [InlineData("An system for managing and synchronizing data between different repositories, including cloud repositories, local repositories, and etc.", 
        "QW4gc3lzdGVtIGZvciBtYW5hZ2luZyBhbmQgc3luY2hyb25pemluZyBkYXRhIGJldHdlZW4gZGlmZmVyZW50IHJlcG9zaXRvcmllcywgaW5jbHVkaW5nIGNsb3VkIHJlcG9zaXRvcmllcywgbG9jYWwgcmVwb3NpdG9yaWVzLCBhbmQgZXRjLg==")]

    public void GivenAString_WhenToBase64StringIsCalled_ThenCorrectBase64StringIsReturned(string input, string expected)
    {
        Assert.Equal(expected, input.ToBase64String());
    }
}