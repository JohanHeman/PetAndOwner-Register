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
    
    [Theory]
    [InlineData(null)] 
    [InlineData("")] 
    [InlineData("         ")] 
    public async Task GetOrSaveCountryAsync_ShouldThrowArgumentException_WhenCountryNameIsNullOrWhiteSpace(string input)
    {
        // Arrange
        var expected = "Value cannot be null or whitespace"; 

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetOrSaveCountryAsync(input));

        // Assert
        Assert.Equal(expected, actual.Message);
    }
    
    [Fact]
    public async Task GetOrSaveCountryAsync_ShouldReturnCountry_WhenMatchFound()
    {
        // Arrange
        var country = new Country()
        {
            Name = "countryName"
        };

        var expectedName = "countryName";
        
        _countryRepo.Setup(x => x.GetCountryByNameAsync(country.Name)).ReturnsAsync(country);

        // Act
        var actual = await _sut.GetOrSaveCountryAsync(country.Name);

        // Assert
        
        _countryRepo.Verify(x => x.GetCountryByNameAsync(country.Name), Times.Once);
        Assert.Equal(actual.Name, expectedName);
        _countryRepo.Verify(x => x.CreateAsync(It.IsAny<Country>()), Times.Never);
    }

    [Fact]
    public async Task GetOrSaveCountryAsync_ShouldCreateCountry_WhenMatchNotFound()
    {
        // Arrange
        var country = new Country(){Name = "countryName"};
        _countryRepo.Setup(x => x.GetCountryByNameAsync(country.Name)).ReturnsAsync((Country?)null);
        
        // Act
        var actual = await _sut.GetOrSaveCountryAsync(country.Name);

        // Assert
        _countryRepo.Verify(x => x.CreateAsync(It.IsAny<Country>()), Times.Once);
        Assert.Equal(actual.Name, country.Name);
    }
    
    
    
    
    
    
}