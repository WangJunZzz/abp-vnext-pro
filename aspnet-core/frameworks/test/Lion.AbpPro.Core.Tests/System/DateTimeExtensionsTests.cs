namespace System;

public class DateTimeExtensionsTests
{
    [Fact]
    public void IsWeekend_ReturnsTrueForWeekends()
    {
        new DateTime(2021, 9, 11).IsWeekend().ShouldBeTrue(); // Saturday
        new DateTime(2021, 9, 12).IsWeekend().ShouldBeTrue(); // Sunday
    }

    [Fact]
    public void IsWeekend_ReturnsFalseForWeekdays()
    {
        new DateTime(2021, 9, 13).IsWeekend().ShouldBeFalse(); // Monday
        new DateTime(2021, 9, 14).IsWeekend().ShouldBeFalse(); // Tuesday
        new DateTime(2021, 9, 15).IsWeekend().ShouldBeFalse(); // Wednesday
        new DateTime(2021, 9, 16).IsWeekend().ShouldBeFalse(); // Thursday
        new DateTime(2021, 9, 17).IsWeekend().ShouldBeFalse(); // Friday
    }
    

    [Fact]
    public void ToCurrentDateMinDateTime_ReturnsExpectedValue()
    {
        var dateTime = new DateTime(2021, 9, 10, 11, 22, 33, 123);
        dateTime.ToCurrentDateMinDateTime().ShouldBe(new DateTime(2021, 9, 10, 0, 0, 0));
    }


    [Fact]
    public void ToUnixTimeSeconds_ReturnsExpectedValue()
    {
        // Arrange
        var dateTime = new DateTime(2021, 9, 15, 12, 34, 56);

        // Act
        var timeStamp = dateTime.ToUnixTimeSeconds();
        
        // Assert
        timeStamp.ToLocalTimeDateBySeconds().ShouldBe(dateTime);
    }

    [Fact]
    public void ToUnixTimeMilliseconds_ReturnsExpectedValue()
    {
        // Arrange
        var dateTime = new DateTime(2021, 9, 15, 12, 34, 56);

        // Act
        var timeStamp = dateTime.ToUnixTimeMilliseconds();
  
        // Assert
        timeStamp.Value.ToLocalTimeDateByMilliseconds().ShouldBe(dateTime);
     
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