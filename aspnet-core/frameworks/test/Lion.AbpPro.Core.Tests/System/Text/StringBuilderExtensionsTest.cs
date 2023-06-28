namespace System.Text
{
    public class StringBuilderExtensionsTest
    {
        [Fact]
        public void TrimStart_RemovesLeadingWhitespace()
        {
            var sb = new StringBuilder("  hello");
            sb.TrimStart();
            sb.ToString().ShouldBe("hello");
        }

        [Fact]
        public void TrimStart_RemovesLeadingChar()
        {
            var sb = new StringBuilder("***hello");
            sb.TrimStart('*');
            sb.ToString().ShouldBe("hello");
        }

        [Fact]
        public void TrimStart_RemovesLeadingChars()
        {
            var sb = new StringBuilder("###hello");
            sb.TrimStart(new char[] { '#' });
            sb.ToString().ShouldBe("hello");
        }

        [Fact]
        public void TrimStart_RemovesLeadingString()
        {
            var sb = new StringBuilder("world of warcraft");
            sb.TrimStart("world of");
            sb.ToString().ShouldBe(" warcraft");
        }

        [Fact]
        public void TrimEnd_RemovesTrailingWhitespace()
        {
            var sb = new StringBuilder("world    ");
            sb.TrimEnd();
            sb.ToString().ShouldBe("world");
        }

        [Fact]
        public void TrimEnd_RemovesTrailingChar()
        {
            var sb = new StringBuilder("hello***");
            sb.TrimEnd('*');
            sb.ToString().ShouldBe("hello");
        }

        [Fact]
        public void TrimEnd_RemovesTrailingChars()
        {
            var sb = new StringBuilder("hello###");
            sb.TrimEnd(new char[] { '#' });
            sb.ToString().ShouldBe("hello");
        }

        [Fact]
        public void TrimEnd_RemovesTrailingString()
        {
            var sb = new StringBuilder("world of warcraft");
            sb.TrimEnd("of warcraft");
            sb.ToString().ShouldBe("world ");
        }

        [Fact]
        public void Trim_RemovesLeadingAndTrailingWhitespace()
        {
            var sb = new StringBuilder("   foo   ");
            sb.Trim();
            sb.ToString().ShouldBe("foo");
        }

        [Fact]
        public void Substring_ReturnsSubstringWithCorrectLength()
        {
            var sb = new StringBuilder("abcdefg");
            sb.SubString(1, 3).ShouldBe("bcd");
        }

        [Fact]
        public void Substring_ThrowsExceptionWhenStartPlusLengthIsGreaterThanLength()
        {
            var sb = new StringBuilder("abcdefg");
            Should.Throw<ArgumentOutOfRangeException>(() => sb.SubString(2, 6));
        }
    }
}