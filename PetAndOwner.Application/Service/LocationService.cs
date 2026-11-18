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
        return default(City);
    }

    public Task<Country> GetOrSaveCountryAsync(string countryName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<City>> GetCitiesInDbAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Country>> GetCountriesInDbAsync()
    {
        throw new NotImplementedException();
    }
}