using Moq;
using PetAndOwner.Application;
using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;
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

    [Fact]
    public async Task GetOrSaveCityAsync_ShouldReturnCity_WhenMatchFound()
    {
        // Arrange
        var city = new City()
        {
            Name = "cityName"
        };

        var expectedName = "cityName";
        
        _cityRepo.Setup(x => x.GetCityByNameAsync(city.Name)).ReturnsAsync(city);

        // Act
        var actual = await _sut.GetOrSaveCityAsync(city.Name);

        // Assert
        
        _cityRepo.Verify(x => x.GetCityByNameAsync(city.Name), Times.Once);
        Assert.Equal(actual.Name, expectedName);
        _cityRepo.Verify(x => x.CreateAsync(It.IsAny<City>()), Times.Never);
    }

    [Fact]
    public async Task GetOrSaveCityAsync_ShouldCreateCity_WhenMatchNotFound()
    {
        // Arrange
        var city = new City(){Name = "cityName"};
        _cityRepo.Setup(x => x.GetCityByNameAsync(city.Name)).ReturnsAsync((City?)null);
        // Act
        var actual = await _sut.GetOrSaveCityAsync(city.Name);

        // Assert
        _cityRepo.Verify(x => x.CreateAsync(It.IsAny<City>()), Times.Once);
    }
    
    
    
}