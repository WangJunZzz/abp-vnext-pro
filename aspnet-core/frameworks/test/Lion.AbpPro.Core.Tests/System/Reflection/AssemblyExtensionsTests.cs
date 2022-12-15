namespace System.Reflection;

public class AssemblyExtensionsTests
{
    [Fact]
    public void GetFileVersionTest()
    {
        var assembly = typeof(AssemblyExtensionsTests).Assembly;
        assembly.GetFileVersion().ShouldBe("1.0.0.0");
    }
}