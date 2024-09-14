namespace FlowSynx.Plugin.Storage.UnitTests;

public class StorageEntityTests
{
    [Theory]
    [InlineData("/", null, "/")]
    [InlineData("f0/f1/f2/", "f0/f1/", "f2")]
    public void GivenStringArray_WhenStorageEntityInitialized_ThenCorrectFullPathIsReturned(string expected, string basePath, string relativePath)
    {
        var entity = new StorageEntity(basePath, relativePath, StorageEntityItemKind.Directory);
        Assert.Equal(expected, entity.FullPath);
    }

    [Theory]
    [InlineData(".txt", "/test1/readme.txt")]
    [InlineData(".png", "/test1/image.png")]
    public void GivenStringArray_WhenGetExtensionIsCalled_ThenCorrectExtensionIsReturned(string expected, string filePath)
    {
        var entity = new StorageEntity(filePath, StorageEntityItemKind.File);
        Assert.Equal(expected, entity.GetExtension());
    }

    [Theory]
    [InlineData("text/plain", "/test1/readme.txt")]
    [InlineData("image/png", "/test1/image.png")]
    [InlineData("image/jpeg", "/test1/image.jpg")]
    public void GivenStringArray_WhenContentTypeIsCalled_ThenCorrectContentTypeIsReturned(string expected, string filePath)
    {
        var entity = new StorageEntity(filePath, StorageEntityItemKind.File);
        Assert.Equal(expected, entity.ContentType);
    }

    [Theory]
    [InlineData("file:readme.txt", "File", "/test1/readme.txt")]
    [InlineData("directory:image", "Directory", "/test1/image/")]
    public void GivenStringArray_WhenToStringIsCalled_ThenCorrectToStringIsReturned(string expected, string kind, string path)
    {
        var entity = new StorageEntity(path, kind);
        var actualValue = $"{kind.ToString().ToLower()}:{entity.Name}";
        Assert.Equal(expected, actualValue);
    }

    [Fact]
    public void GivenStringArray_WhenTryAddMetadataIsCalled_ThenCorrectMetadataIsReturned()
    {
        var keyValues = new object[]
        {
            "Key1", "Value1",
            "Key2", "Value2",
            "Key3", "value3"
        };

        var entity = new StorageEntity("test", StorageEntityItemKind.File);
        entity.TryAddMetadata(keyValues);
        Assert.Equal(3, entity.Metadata.Count);
        Assert.True(entity.Metadata.ContainsKey("Key2"));
    }
}