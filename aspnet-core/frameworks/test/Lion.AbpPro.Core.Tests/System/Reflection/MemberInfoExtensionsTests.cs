using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System.Reflection;

public class MemberInfoExtensionsTests
{
    private class TestClass
    {
        [Description("Test property")]
        public string TestProperty { get; set; }
        
        [DisplayName("Test method")]
        public void TestMethod() {}
        
        [Display(Name = "Test field")]
        public int TestField;
    }
    
    [Fact]
    public void GetDescription_ReturnsDescriptionAttribute_WhenExists()
    {
        // Arrange
        var memberInfo = typeof(TestClass).GetProperty(nameof(TestClass.TestProperty));
        
        // Act
        var result = memberInfo.GetDescription();
        
        // Assert
        result.ShouldBe("Test property");
    }
    
    [Fact]
    public void GetDescription_ReturnsDisplayNameAttribute_WhenDescriptionAttributeNotExists()
    {
        // Arrange
        var memberInfo = typeof(TestClass).GetMethod(nameof(TestClass.TestMethod));
        
        // Act
        var result = memberInfo.GetDescription();
        
        // Assert
        result.ShouldBe("Test method");
    }
    
    [Fact]
    public void GetDescription_ReturnsDisplayAttribute_WhenBothDescriptionAndDisplayNameAttributesNotExists()
    {
        // Arrange
        var memberInfo = typeof(TestClass).GetField(nameof(TestClass.TestField));
        
        // Act
        var result = memberInfo.GetDescription();
        
        // Assert
        result.ShouldBe("Test field");
    }
}