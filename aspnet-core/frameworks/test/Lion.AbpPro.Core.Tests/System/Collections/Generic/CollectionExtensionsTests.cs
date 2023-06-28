namespace System.Collections.Generic;

public class CollectionExtensionsTests
{
    [Fact]
    public void AddIf_WithFlagTrue_ShouldAddItem()
    {
        // Arrange
        var collection = new List<int>();

        // Act
        collection.AddIf(1, true);

        // Assert
        collection.ShouldContain(1);
    }

    [Fact]
    public void AddIf_WithFlagFalse_ShouldNotAddItem()
    {
        // Arrange
        var collection = new List<int>();

        // Act
        collection.AddIf(1, false);

        // Assert
        collection.ShouldBeEmpty();
    }

    [Fact]
    public void AddIf_WithFuncReturningTrue_ShouldAddItem()
    {
        // Arrange
        var collection = new List<int>();
        Func<bool> func = () => true;

        // Act
        collection.AddIf(1, func);

        // Assert
        collection.ShouldContain(1);
    }

    [Fact]
    public void AddIf_WithFuncReturningFalse_ShouldNotAddItem()
    {
        // Arrange
        var collection = new List<int>();
        Func<bool> func = () => false;

        // Act
        collection.AddIf(1, func);

        // Assert
        collection.ShouldBeEmpty();
    }

    [Fact]
    public void AddIf_WithNullCollection_ShouldThrowArgumentNullException()
    {
        // Arrange
        ICollection<int> collection = null;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => collection.AddIf(1, true));
    }
    
    
    [Theory]
    [InlineData(true, new int[] { 1, 2, 3 }, 4)]
    [InlineData(false, new int[] { 1, 2, 3 }, 2)]
    public void AddIf_WhenFuncReturnsTrue_AddsValueToCollection(bool exists, IEnumerable<int> existingValues, int valueToAdd)
    {
        // Arrange
        var collection = new List<int>(existingValues);
        var func = () => exists ? existingValues.Contains(valueToAdd) : !existingValues.Contains(valueToAdd);

        // Act
        collection.AddIf(valueToAdd, func);

        // Assert
        if (exists)
        {
            collection.ShouldBe(existingValues);
        }
        else
        {
            collection.ShouldContain(valueToAdd);
        }
    }
    
    [Fact]
    public void AddIfNotExist_WhenCollectionIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ICollection<int> collection = null;
        var value = 42;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => collection.AddIfNotExist(value));
    }

    [Fact]
    public void AddIfNotExist_WhenValueExistsInCollection_AddsNothingToCollection()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3 };
        var valueToAdd = 2;

        // Act
        collection.AddIfNotExist(valueToAdd);

        // Assert
        collection.ShouldBe(new[] { 1, 2, 3 });
    }

    [Fact]
    public void AddIfNotExist_WhenValueDoesNotExistInCollection_AddsValueToCollection()
    {
        // Arrange
        var collection = new List<int> { 1, 2, 3 };
        var valueToAdd = 4;

        // Act
        collection.AddIfNotExist(valueToAdd);

        // Assert
        collection.ShouldBe(new[] { 1, 2, 3, 4 });
    }

    [Fact]
    public void AddIfNotExist_WhenExistFuncIsProvided_UsesExistFuncToDetermineIfValueExists()
    {
        // Arrange
        ICollection<string> collection = new List<string> { "Foo", "Bar" };
        var valueToAdd = "baz";
        Func<string, bool> existFunc = s => s.StartsWith("b");

        // Act
        collection.AddIfNotExist(valueToAdd, existFunc);

        // Assert
        collection.ShouldBe(new[] { "Foo", "Bar", "baz" });
    }
    
    [Fact]
    public void AddIfNotNull_WhenCollectionIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ICollection<string> collection = null;
        var valueToAdd = "Foo";

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => collection.AddIfNotNull(valueToAdd));
    }

    [Fact]
    public void AddIfNotNull_WhenValueIsNull_DoesNotAddValueToCollection()
    {
        // Arrange
        var collection = new List<string>();
        string valueToAdd = null;

        // Act
        collection.AddIfNotNull(valueToAdd);

        // Assert
        collection.ShouldBeEmpty();
    }

    [Fact]
    public void AddIfNotNull_WhenValueIsNotNull_AddsValueToCollection()
    {
        // Arrange
        var collection = new List<string>();
        var valueToAdd = "Foo";

        // Act
        collection.AddIfNotNull(valueToAdd);

        // Assert
        collection.ShouldBe(new[] { "Foo" });
    }
    
    [Fact]
    public void GetOrAdd_Should_Return_Existing_Item()
    {
        // arrange
        var collection = new List<string> { "existing item" };
        var factory = new FakeFactory();

        // act
        var result = collection.GetOrAdd(item => item == "existing item", factory.Create);

        // assert
        result.ShouldBe("existing item");
        collection.Count.ShouldBe(1);
    }

    [Fact]
    public void GetOrAdd_Should_Add_New_Item()
    {
        // arrange
        var collection = new List<string>();
        var factory = new FakeFactory();

        // act
        var result = collection.GetOrAdd(item => item == "new item", factory.Create);

        // assert
        result.ShouldBe("new item");
        collection.ShouldContain(result);
    }

    [Fact]
    public void IsContinuous_WithNull_ReturnsFalse()
    {
        // Arrange
        List<int> numList = null;

        // Act
        bool result = numList.IsContinuous();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsContinuous_WithEmptyList_ReturnsFalse()
    {
        // Arrange
        List<int> numList = new List<int>();

        // Act
        bool result = numList.IsContinuous();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsContinuous_WithNonSequentialNumbers_ReturnsFalse()
    {
        // Arrange
        List<int> numList = new List<int> { 1, 3, 5, 7 };

        // Act
        bool result = numList.IsContinuous();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsContinuous_WithSequentialNumbers_ReturnsTrue()
    {
        // Arrange
        List<int> numList = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        bool result = numList.IsContinuous();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsContinuous_WithReverseSequentialNumbers_ReturnsTrue()
    {
        // Arrange
        List<int> numList = new List<int> { 5, 4, 3, 2, 1 };

        // Act
        bool result = numList.IsContinuous();

        // Assert
        result.ShouldBeTrue();
    }
    
    private class FakeFactory
    {
        public string Create() => "new item";
    }
}