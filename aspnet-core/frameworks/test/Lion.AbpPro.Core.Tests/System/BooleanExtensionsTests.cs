namespace System;

public class BooleanExtensionsTests
{
    [Fact]
    public void ToLowerTest()
    {
        true.ToLower().ShouldBe("true");
        false.ToLower().ShouldBe("false");
    }

    [Fact]
    public void TrueThrowTest()
    {
        Should.Throw<Exception>(() =>
        {
            true.TrueThrow(new Exception());
        });

        false.TrueThrow(new Exception());
    }
}