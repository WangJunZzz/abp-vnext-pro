namespace System;

public class DecimalExtensionsTest
{
    [Fact]
    public void TrimEndZero()
    {
        var d1 = 0.1M;
        var d2 = 1M;
        var d3 = 1.001M;
        var d4 = 1.0000M;
        var d5 = 0.1010M;
        var result1 = d1.TrimEndZero();
        var result2 = d2.TrimEndZero();
        var result3 = d3.TrimEndZero();
        var result4 = d4.TrimEndZero();
        var result5 = d5.TrimEndZero();
        
        result1.ToString().ShouldBe("0.1");
        result2.ToString().ShouldBe("1");
        result3.ToString().ShouldBe("1.001");
        result4.ToString().ShouldBe("1");
        result5.ToString().ShouldBe("0.101");
   
    }
}