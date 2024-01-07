using FlowSynx.Reflections.UnitTests.TestImplementations;
using Newtonsoft.Json.Linq;

namespace FlowSynx.Reflections.UnitTests;

public class PropertyExtensionsTests
{
    [Fact]
    public void GivenATypeAndPropertyName_WhenPropertyIsCalled_ThenPropertyInfoReturned()
    {
        var type = typeof(TestClass);
        var propertyName = "FullName";
        var propertyInfo = type.Property(propertyName);
        Assert.NotNull(propertyInfo);
        Assert.Equal(propertyName, propertyInfo.Name);
    }

    [Fact]
    public void GivenAType_WhenPropertiesIsCalled_ThenListOfPropertyInfoReturned()
    {
        var type = typeof(TestClass);
        var propertyName = "FullName";
        var propertyInfo = type.Properties();
        Assert.NotNull(propertyInfo);
        Assert.Equal(2, propertyInfo.Count);
        Assert.Equal(propertyName, propertyInfo.First(x=>x.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)).Name);
    }

    [Fact]
    public void GivenAnObjectAndPropertyName_WhenGetPropertyValueIsCalled_ThenPropertyValueReturned()
    {
        var type = new TestClass();
        var propertyName = "FullName";
        var propertyValue = type.GetPropertyValue(propertyName);
        Assert.NotNull(propertyValue);
        Assert.Equal("TestName", propertyValue.ToString());
    }

    [Fact]
    public void GivenAPropertyInfoAndPropertyName_WhenGetPropertyValueIsCalled_ThenPropertyValueReturned()
    {
        var type = new TestClass();
        var propertyName = "FullName";
        var propertyInfo = type.GetType().Property(propertyName);
        Assert.NotNull(propertyInfo);
        var propertyValue = propertyInfo.GetPropertyValue(type);
        Assert.NotNull(propertyValue);
        Assert.Equal("TestName", propertyValue.ToString());
    }

    [Fact]
    public void GivenAPropertyInfo_WhenGetPropertyTypeIsCalled_ThenTypeReturned()
    {
        var type = new TestClass();
        var propertyName = "FullName";
        var propertyInfo = type.GetType().Property(propertyName);
        Assert.NotNull(propertyInfo);
        var propertyType = propertyInfo.GetPropertyType();
        Assert.NotNull(propertyType);
        Assert.Equal(typeof(string), propertyType);
    }

    [Fact]
    public void GivenAPropertyInfo_WhenIsGenericTypeIsCalled_ThenBooleanValueReturned()
    {
        var type = new TestClass();
        var propertyName = "Specifications";
        var propertyInfo = type.GetType().Property(propertyName);
        Assert.NotNull(propertyInfo);
        var isGenericType = propertyInfo.IsGenericType();
        Assert.True(isGenericType);
    }

    [Fact]
    public void GivenAPropertyInfo_WhenIsDictionaryTypeIsCalled_ThenBooleanValueReturned()
    {
        var type = new TestClass();
        var propertyName = "Specifications";
        var propertyInfo = type.GetType().Property(propertyName);
        Assert.NotNull(propertyInfo);
        var isDictionaryType = propertyInfo.IsDictionaryType();
        Assert.True(isDictionaryType);
    }
}