using FlowSynx.IO.Cache;

namespace FlowSynx.IO.UnitTests.Cache;

public class MultiKeyCacheTests
{
    private readonly IMultiKeyCache<string, string, TestCacheItem> _cache;

    public MultiKeyCacheTests()
    {
        _cache = new MultiKeyCache<string, string, TestCacheItem>();

        _cache.Set("ZqR1DQV9r9", "WUdBt80zVL", new TestCacheItem {
            Id = Guid.Parse("6eb45e1f-090c-4937-b304-f5bf575cc87a"), 
            Name = "FlowSynx Engine v0.1.0"
        });

        _cache.Set("ZqR1DQV9r9", "SYyK1E8kz0", new TestCacheItem
        {
            Id = Guid.Parse("4005ca7c-15e2-4642-995b-2d39b3d2b6b5"),
            Name = "FlowSynx Engine v0.2.0"
        });

        _cache.Set("2bqow9TY2u", "Gsg1iXjzo6", new TestCacheItem
        {
            Id = Guid.Parse("052a6198-f64b-49b2-86c4-c31cac2311dd"),
            Name = "FlowSynx Dashboard"
        });
    }

    [Fact]
    public void Get_PrimaryKeyNotFound_ReturnsNull()
    {
        var primaryKey = "217EE638351B2";
        var secondaryKey = "WUdBt80zVL";
        var cacheItem = _cache.Get(primaryKey, secondaryKey);
        Assert.Null(cacheItem);
    }

    [Fact]
    public void Get_PrimaryAndSecondaryKeysFound_ReturnsItem()
    {
        var primaryKey = "ZqR1DQV9r9";
        var secondaryKey = "WUdBt80zVL";
        var cacheItem = _cache.Get(primaryKey, secondaryKey);
        Assert.NotNull(cacheItem);
        Assert.Equal("6eb45e1f-090c-4937-b304-f5bf575cc87a", cacheItem.Id.ToString());
        Assert.Equal("FlowSynx Engine v0.1.0", cacheItem.Name);
    }

    [Theory]
    [InlineData(2, "ZqR1DQV9r9")]
    [InlineData(1, "2bqow9TY2u")]
    [InlineData(0, "217EE638351B2")]

    public void Count_PrimaryKeyFount_ReturnsCorrectCount(int expected, string primaryKey)
    {
        var actual = _cache.Count(primaryKey);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Set_PrimaryAndSecondaryKeysInserted_ReturnsCorrectCountAndValue()
    {
        var primaryKey = "57999BDC5B8AD";
        var secondaryKey = "9691BAC7D3A4B";
        var item = new TestCacheItem { 
            Id = Guid.Parse("f0e399b2-6588-4b5e-b37c-b511e47d033f"), 
            Name = "FlowCtl v0.2.0"
        };

        _cache.Set(primaryKey, secondaryKey, item);
        var count = _cache.Count(primaryKey);
        Assert.Equal(1, count);

        var cacheItem = _cache.Get(primaryKey, secondaryKey);
        Assert.NotNull(cacheItem);
        Assert.Equal("f0e399b2-6588-4b5e-b37c-b511e47d033f", cacheItem.Id.ToString());
        Assert.Equal("FlowCtl v0.2.0", cacheItem.Name);
    }

    internal class TestCacheItem
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}