using System.Text;

namespace FlowSynx.IO.UnitTests;

public class ByteExtensionsTests
{
    [Theory]
    [InlineData("FlowSynx", "Rmxvd1N5bng=")]

    [InlineData("Syncs and manage your files to different storage", 
        "U3luY3MgYW5kIG1hbmFnZSB5b3VyIGZpbGVzIHRvIGRpZmZlcmVudCBzdG9yYWdl")]

    [InlineData("An system for managing and synchronizing data between different repositories, including cloud repositories, local repositories, and etc.", 
        "QW4gc3lzdGVtIGZvciBtYW5hZ2luZyBhbmQgc3luY2hyb25pemluZyBkYXRhIGJldHdlZW4gZGlmZmVyZW50IHJlcG9zaXRvcmllcywgaW5jbHVkaW5nIGNsb3VkIHJlcG9zaXRvcmllcywgbG9jYWwgcmVwb3NpdG9yaWVzLCBhbmQgZXRjLg==")]

    public void GivenBytesArray_WhenToBase64StringIsCalled_ThenCorrectBase64StringIsReturned(string input, string expected)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        Assert.Equal(expected, bytes.ToBase64String());
    }
}