namespace System;

public class DateTimeExtensionsTests
{
    [Fact]
    public void IsWeekendTest()
    {
        var dt = new DateTime(2021, 4, 24);
        dt.IsWeekend().ShouldBeTrue();
        dt = new DateTime(2021, 4, 25);
        dt.IsWeekend().ShouldBeTrue();
        for (var i = 1; i <= 5; i++)
        {
            dt = new DateTime(2021, 4, 25 + i);
            dt.IsWeekend().ShouldBeFalse();
        }
    }

    [Fact]
    public void IsWeekdayTest()
    {
        var dt = new DateTime(2021, 4, 24);
        dt.IsWeekday().ShouldBeFalse();
        dt = new DateTime(2021, 4, 25);
        dt.IsWeekday().ShouldBeFalse();
        for (var i = 1; i <= 5; i++)
        {
            dt = new DateTime(2021, 4, 25 + i);
            dt.IsWeekday().ShouldBeTrue();
        }
    }

    [Fact]
    public void ToUniqueStringTest()
    {
        var dt = new DateTime(2021, 4, 24,21,30,23);
        dt.ToUniqueString().ShouldBe("202111477423");
        dt = dt.AddMilliseconds(-1);
        dt.ToUniqueString(true).ShouldBe("202111477422999");
    }

    [Fact]
    public void ToYyyyMMddTest()
    {
        var dt1 = new DateTime(2021, 9, 1);
        dt1.ToYyyyMmDd().ShouldBe(20210901);
            
        var dt2 = new DateTime(2020, 12, 31);
        dt2.ToYyyyMmDd().ShouldBe(20201231);
    }

    [Fact]
    public void ToYyyyMMTest()
    {
        var dt1 = new DateTime(2021, 9, 6);
        dt1.ToYyyyMm().ShouldBe(202109);
            
        var dt2 = new DateTime(2020, 12, 31);
        dt2.ToYyyyMm().ShouldBe(202012);
    }
}