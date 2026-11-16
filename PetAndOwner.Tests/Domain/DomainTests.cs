using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Tests;

public class DomainTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AllergyName_ShouldThrowArgumentException_WhenNameIsNullorWhiteSpace(string? name)
    {
        // Arrange
        Allergy allergy = new Allergy();
        var expectedMessage = "Name can't be null or whitespace.";

        // Act
        var actual = Assert.Throws<ArgumentException>(() => allergy.Name = name);

        // Assert
        Assert.Equal(expectedMessage, actual.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AnimalTypeName_ShouldThrowArgumentException_WhenNameIsNullorWhiteSpace(string? name)
    {
        // Arrange
        AnimalType animalType = new AnimalType();
        var expectedMessage = "Name can't be null or whitespace";

        // Act
        var actual = Assert.Throws<ArgumentException>(() => animalType.Name = name);

        // Assert
        Assert.Equal(expectedMessage, actual.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CountryName_ShouldThrowArgumentException_WhenNameIsNullorWhiteSpace(string? name)
    {
        // Arrange
        Country country = new Country();
        var expectedMessage = "Name can't be null or whitespace";

        // Act
        var actual = Assert.Throws<ArgumentException>(() => country.Name = name);

        // Assert
        Assert.Equal(expectedMessage, actual.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FoodName_ShouldThrowArgumentException_WhenNameIsNullorWhiteSpace(string? name)
    {
        // Arrange
        Food food = new Food();
        var expectedMessage = "Name can't be null or whitespace";

        // Act
        var actual = Assert.Throws<ArgumentException>(() => food.Name = name);

        // Assert
        Assert.Equal(expectedMessage, actual.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FoodBrandName_ShouldThrowArgumentException_WhenNameIsNullorWhiteSpace(string? name)
    {
        // Arrange
        FoodBrand foodBrand = new FoodBrand();
        var expectedMessage = "Name can't be null or whitespace";

        // Act
        var actual = Assert.Throws<ArgumentException>(() => foodBrand.Name = name);

        // Assert
        Assert.Equal(expectedMessage, actual.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void PetName_ShouldThrowArgumentException_WhenNameIsNullorWhiteSpace(string? name)
    {
        // Arrange

        Pet pet = new Pet();
        var expectedMessage = "Name can't be null or whitespace";

        // Act

        var actual = Assert.Throws<ArgumentException>(() => pet.Name = name);

        // Assert

        Assert.Equal(expectedMessage, actual.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-55)]
    public void PetAge_ShouldThrowArgumentException_WhenAgeIsInvalid(int age)
    {
        // Arrange 
        Pet pet = new Pet();
        var expectedMessage = "Age can't be less than or equal to zero";
        // Act
        var actual = Assert.Throws<ArgumentException>(() => pet.Age = age);

        // Assert 
        Assert.Equal(expectedMessage, actual.Message);
    }
}