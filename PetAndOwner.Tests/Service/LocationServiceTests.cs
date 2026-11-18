using Moq;
using PetAndOwner.Application;
using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Interface.Repository;

namespace PetAndOwner.Tests.Service;

public class LocationServiceTests
{
    private readonly Mock<ICountryRepository> _countryRepo;
    private readonly Mock<ICityRepository> _cityRepo;
    private readonly ILocationService _sut;
    public LocationServiceTests()
    {
       _countryRepo = new Mock<ICountryRepository>(); 
       _cityRepo = new Mock<ICityRepository>();
       _sut = new LocationService(
        _countryRepo.Object,
        _cityRepo.Object);
    }

    [Theory]
    [InlineData(null)] 
    [InlineData("")] 
    [InlineData("         ")] 
    public async Task GetOrSaveCityAsync_ShouldThrowArgumentException_WhenCityNameIsNullOrWhiteSpace(string input)
    {
        // Arrange
        var expected = "Value cannot be null or whitespace"; 

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetOrSaveCityAsync(input));

        // Assert
        Assert.Equal(expected, actual.Message);
    }
    
    
}