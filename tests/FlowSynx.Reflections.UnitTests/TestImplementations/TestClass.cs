namespace FlowSynx.Reflections.UnitTests.TestImplementations;

internal class TestClass : ITestInterface
{
    public string FullName { get; set; } = "TestName";
    public Dictionary<string, string?>? Specifications { get; set; }
}