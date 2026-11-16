using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Tests;

public class AllergyTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AllergyName_ShouldThrowArgumentException_whenNameIsNullorWhiteSpace(string name)
    {
        // Arrange
        Allergy allergy = new Allergy(); 
        var expectedMessage = "Name cannot be null or whitespace."; 

        // Act
        var actual = Assert.Throws<ArgumentException>(() => allergy.Name = name);

        // Assert
        Assert.Equal(expectedMessage, actual.Message);
    }
    
}