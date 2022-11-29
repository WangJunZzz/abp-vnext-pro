namespace System.Text;

public class StringBuilderExtensionsTest
{
    [Fact]
    public void TrimTest()
    {
        var sb = new StringBuilder("   hello world  ");
        sb.Trim().ToString().ShouldBe("hello world");
    }

    [Fact]
    public void TrimStartTest()
    {
        var sb = new StringBuilder();
        sb.TrimStart('a').ToString().ShouldBe(string.Empty);

        sb.Append("asdfgef");
        sb.TrimStart('a').ToString().ShouldBe("sdfgef");

        sb.Insert(0, "   ");
        sb.TrimStart().ToString().ShouldBe("sdfgef");

        sb.TrimStart("sdf").ToString().ShouldBe("gef");

        sb.TrimStart("gef").ToString().ShouldBe(string.Empty);
    }

    [Fact]
    public void TrimEndTest()
    {
        var sb = new StringBuilder("asdfgef");

        sb.TrimEnd((string)null).ToString().ShouldBe("asdfgef");

        sb.TrimEnd('a').ToString().ShouldBe("asdfgef");

        sb.TrimEnd('f').ToString().ShouldBe("asdfge");

        sb.Append("   ");
        sb.TrimEnd().ToString().ShouldBe("asdfge");

        sb.TrimEnd(new[] { 'g', 'e' }).ToString().ShouldBe("asdf");
        sb.TrimEnd("asdf").ToString().ShouldBe(string.Empty);
    }

    [Fact]
    public void SubStringTest()
    {
        var sb = new StringBuilder("asdfgef");
        Should.Throw<ArgumentOutOfRangeException>(() =>
        {
            sb.SubString(0, 8);
        });
        sb.SubString(0, 3).ToString().ShouldBe("asd");

    }
}