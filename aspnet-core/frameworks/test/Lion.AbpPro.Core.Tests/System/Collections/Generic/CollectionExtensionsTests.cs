namespace System.Collections.Generic;

public class CollectionExtensionsTests
{
    [Fact]
    public void AddIfNotExistTest()
    {
        var numbers = new List<int>() { 1, 2, 3 };
        numbers.AddIfNotExist(2);
        numbers.Count(m => m == 2).ShouldBe(1);
        numbers.AddIfNotExist(5);
        numbers.Count(m => m == 5).ShouldBe(1);

        numbers = null;
        Should.Throw<ArgumentNullException>(() =>
        {
            numbers.AddIfNotExist(3);
        });
    }

    [Fact]
    public void AddIfNotNullTest()
    {
        var strings = new List<string>() { "abc", "bcd", "cde" };
        strings.AddIfNotNull(null);
        strings.Count.ShouldBe(3);
        strings.AddIfNotNull("abc");
        strings.Count.ShouldBe(4);

        strings = null;
        Should.Throw<ArgumentNullException>(() =>
        {
            strings.AddIfNotNull("abc");
        });
    }
}