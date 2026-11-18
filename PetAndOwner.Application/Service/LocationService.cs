using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;

namespace PetAndOwner.Application;

public class LocationService : ILocationService
{
    private readonly ICountryRepository _countryRepository;
    private readonly ICityRepository _cityRepository;

    public LocationService(ICountryRepository countryRepository, ICityRepository cityRepository)
    {
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
    }
    
    public async Task<City> GetOrSaveCityAsync(string cityName)
    {
        if (string.IsNullOrWhiteSpace(cityName)) throw new ArgumentException("Value cannot be null or whitespace");
        var city = await _cityRepository.GetCityByNameAsync(cityName);
        if (city == null)
        {
            City newCity = new City()
            {
                Id = Guid.NewGuid(),
                Name = cityName,
                Country = new Country(),
            };
            await _cityRepository.CreateAsync(newCity);
        }
        return city;
    }

    public async Task<Country> GetOrSaveCountryAsync(string countryName)
    {
        if (string.IsNullOrWhiteSpace(countryName)) throw new ArgumentException("Value cannot be null or whitespace");
        var country = await _countryRepository.GetCountryByNameAsync(countryName);
        if (country == null)
        {
            Country newCountry = new Country()
            {
                Id = Guid.NewGuid(),
                Name = countryName,
            };
            await _countryRepository.CreateAsync(newCountry); 
            country = newCountry;
        }
        return country;
    }

    public async Task<List<City>> GetCitiesInDbAsync()
    {
        return await _cityRepository.GetAllAsync();
    }

    public async Task<List<Country>> GetCountriesInDbAsync()
    {
        return await _countryRepository.GetAllAsync();
    }
}