namespace System.Reflection;

public class MethodInfoExtensionsTests
{
      private abstract class BaseClass
    {
        public virtual void Method1() {}
        
        public virtual async Task<int> Method2Async() { await Task.Delay(1); return 42; }
        
        public void Method3() {}
    }
    
    private class DerivedClass : BaseClass
    {
        public override void Method1() {}
        
        public override async Task<int> Method2Async() { await Task.Delay(1); return 43; }
        
        public new void Method3() {}
    }
    
    [Fact]
    public void IsAsync_ReturnsFalse_WhenReturnTypeIsNotTask()
    {
        // Arrange
        var methodInfo = typeof(BaseClass).GetMethod(nameof(BaseClass.Method1));
        
        // Act
        var result = methodInfo.IsAsync();
        
        // Assert
        result.ShouldBeFalse();
    }
    
    [Fact]
    public void IsAsync_ReturnsTrue_WhenReturnTypeIsTaskOfT()
    {
        // Arrange
        var methodInfo = typeof(BaseClass).GetMethod(nameof(BaseClass.Method2Async));
        
        // Act
        var result = methodInfo.IsAsync();
        
        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsOverridden_ReturnsFalse_WhenMethodIsNotOverridden()
    {
        // Arrange
        var methodInfo = typeof(BaseClass).GetMethod(nameof(BaseClass.Method3));
        
        // Act
        var result = methodInfo.IsOverridden();
        
        // Assert
        result.ShouldBeFalse();
    }
    
    [Fact]
    public void IsOverridden_ReturnsTrue_WhenMethodIsOverridden()
    {
        // Arrange
        var methodInfo = typeof(DerivedClass).GetMethod(nameof(DerivedClass.Method1));
        
        // Act
        var result = methodInfo.IsOverridden();
        
        // Assert
        result.ShouldBeTrue();
    }
    
    [Fact]
    public void IsOverridden_ReturnsFalse_WhenMethodIsHiddenByNew()
    {
        // Arrange
        var methodInfo = typeof(DerivedClass).GetMethod(nameof(DerivedClass.Method3));
        
        // Act
        var result = methodInfo.IsOverridden();
        
        // Assert
        result.ShouldBeFalse();
    }
}