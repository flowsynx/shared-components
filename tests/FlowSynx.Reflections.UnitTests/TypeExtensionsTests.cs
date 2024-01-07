using FlowSynx.Reflections.UnitTests.TestImplementations;

namespace FlowSynx.Reflections.UnitTests;

public class TypeExtensionsTests
{
    [Fact]
    public void GivenAType_WhenGetInterfacesIsCalled_ThenListOfInterfacesTypesReturned()
    {
        var expectedType = typeof(ITestInterface);
        var actualType = typeof(TestClass);
        Assert.Equal(expectedType, actualType.GetInterfaces().First());
    }

    [Fact]
    public void GivenAType_WhenIsInterfaceIsCalled_ThenCheckItIsInterfaceType()
    {
        var type = typeof(ITestInterface);
        Assert.True(type.IsInterface);
    }

    [Fact]
    public void GivenAType_WhenIsClassIsCalled_ThenCheckItIsClassType()
    {
        var type = typeof(TestClass);
        Assert.True(type.IsClass);
    }

    [Fact]
    public void GivenAType_WhenIsGenericTypeIsCalled_ThenCheckItIsGenericType()
    {
        var type = typeof(TestGenericClass<>);
        Assert.True(type.IsGenericType());
    }

    [Fact]
    public void GivenAType_WhenIsDictionaryTypeIsCalled_ThenCheckItIsDictionaryType()
    {
        var type = typeof(TestGenericDictionaryClass);
        Assert.True(type.IsDictionaryType());
    }

    [Fact]
    public void GivenAType_WhenIsAssignableIsCalled_ThenCheckItIsIsAssignable()
    {
        var abstraction = typeof(TestClass);
        var concretion = typeof(ITestInterface);
        Assert.True(abstraction.IsAssignable(concretion));
    }
}