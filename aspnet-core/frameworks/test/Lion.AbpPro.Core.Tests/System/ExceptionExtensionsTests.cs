using System.Text;

namespace System;

public class ExceptionExtensionsTests
{
    [Fact]
    public void FormatMessage_ReturnsFormattedString()
    {
        // Arrange
        var exception = new Exception("Test exception");
        exception.Data["CustomData"] = "Custom value";
        var isHideStackTrace = false;
        var expectedMessage = new StringBuilder()
            .AppendLine("异常消息：Test exception")
            .AppendLine("异常类型：System.Exception")
            .AppendLine("异常方法：")
            .AppendLine("异常源：")
            .AppendLine("异常堆栈：   at YourNamespace.Tests.ExceptionExtensionsTests.FormatMessage_ReturnsFormattedString()")
            .AppendLine("内部异常：")
            .ToString();

        // Act
        var result = exception.FormatMessage(isHideStackTrace);

        // Assert
        result.ShouldBe(expectedMessage);
    }

    // [Fact]
    // public void ReThrow_RethrowsException()
    // {
    //     // Arrange
    //     var originalException = new Exception("Original exception");
    //
    //     // Act & Assert
    //     Should.Throw<Exception>(() => originalException.ReThrow());
    // }
    //

    [Fact]
    public void ThrowIf_ThrowsExceptionIfConditionIsTrue()
    {
        // Arrange
        var exception = new InvalidOperationException("Test exception");
        var isThrow = true;

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => exception.ThrowIf(isThrow));
    }

    [Fact]
    public void ThrowIf_DoesNotThrowExceptionIfConditionIsFalse()
    {
        // Arrange
        var exception = new InvalidOperationException("Test exception");
        var isThrow = false;

        // Act & Assert
        exception.ThrowIf(isThrow); // Should not throw an exception
    }

    [Fact]
    public void ThrowIf_ThrowsExceptionIfConditionFunctionIsTrue()
    {
        // Arrange
        var exception = new InvalidOperationException("Test exception");

        // Act & Assert
        Should.Throw<InvalidOperationException>(() => exception.ThrowIf(() => true));
    }

    [Fact]
    public void ThrowIf_DoesNotThrowExceptionIfConditionFunctionIsFalse()
    {
        // Arrange
        var exception = new InvalidOperationException("Test exception");

        // Act & Assert
        exception.ThrowIf(() => false); // Should not throw an exception
    }
}