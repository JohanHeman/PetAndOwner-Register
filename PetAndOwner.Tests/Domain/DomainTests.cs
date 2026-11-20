using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Tests.Domain;

public class DomainTests
{

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

    [Theory]
    [InlineData("")]
    [InlineData("aaaaaaaaaaalaaaaaaaaaaaaaaaaaaaaaaaaaaaaaddddd")]
    [InlineData("abjdwdhawjdwadwakdhwadkjwahdkwajdwakdhwkdhadkjwadwadwakdhwakjdhwakdhwakjdhwad")]
    public void UserName_ShouldThrowArgumentOutOfRangeException_WhenUserNameIsInvalid(string userName)
    {
        // Arrange
        var user = TestUsers.CreateTestUser();
        var expectedMessage = "The name must be between 1 and 30 characters";
        // Act
        var actual = Assert.Throws<ArgumentException>(() => user.Name = userName);

        // Assert
        Assert.Equal(expectedMessage, actual.Message);
    }



}