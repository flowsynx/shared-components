using FlowSynx.IO.Cache;

namespace FlowSynx.IO.UnitTests.Cache;

public class FlowSynxCacheTests
{
    private readonly ICache<string, TestCacheItem> _cache;

    public FlowSynxCacheTests()
    {
        _cache = new FlowSynxCache<string, TestCacheItem>();

        _cache.Set("ZqR1DQV9r9", new TestCacheItem {
            Id = Guid.Parse("6eb45e1f-090c-4937-b304-f5bf575cc87a"), 
            Name = "FlowSynx Engine v0.1.0"
        });

        _cache.Set("2bqow9TY2u", new TestCacheItem
        {
            Id = Guid.Parse("052a6198-f64b-49b2-86c4-c31cac2311dd"),
            Name = "FlowSynx Dashboard"
        });
    }

    [Fact]
    public void Get_KeyNotFound_ReturnsNull()
    {
        var primaryKey = "217EE638351B2";
        var cacheItem = _cache.Get(primaryKey);
        Assert.Null(cacheItem);
    }

    [Fact]
    public void Get_KeysFound_ReturnsItem()
    {
        var primaryKey = "ZqR1DQV9r9";
        var cacheItem = _cache.Get(primaryKey);
        Assert.NotNull(cacheItem);
        Assert.Equal("6eb45e1f-090c-4937-b304-f5bf575cc87a", cacheItem.Id.ToString());
        Assert.Equal("FlowSynx Engine v0.1.0", cacheItem.Name);
    }

    [Fact]
    public void Set_KeysInserted_ReturnsCorrectCountAndValue()
    {
        var primaryKey = "57999BDC5B8AD";
        var item = new TestCacheItem { 
            Id = Guid.Parse("f0e399b2-6588-4b5e-b37c-b511e47d033f"), 
            Name = "FlowCtl v0.2.0"
        };

        _cache.Set(primaryKey, item);
        var count = _cache.Count();
        Assert.Equal(3, count);

        var cacheItem = _cache.Get(primaryKey);
        Assert.NotNull(cacheItem);
        Assert.Equal("f0e399b2-6588-4b5e-b37c-b511e47d033f", cacheItem.Id.ToString());
        Assert.Equal("FlowCtl v0.2.0", cacheItem.Name);
    }

    [Fact]
    public void Set_KeysDeletet_ReturnsCorrectCountAndValue()
    {
        var primaryKey = "2bqow9TY2u";
        _cache.Delete(primaryKey);
        var count = _cache.Count();
        Assert.Equal(1, count);
    }

    internal class TestCacheItem
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}