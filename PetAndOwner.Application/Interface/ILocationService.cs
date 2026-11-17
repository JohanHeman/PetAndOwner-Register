using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Application.Interface;

public interface ILocationService
{
    public Task<City> GetOrSaveCityAsync(string cityName);
    public Task<Country> GetOrSaveCountryAsync(string countryName);

    public Task<IEnumerable<City>> GetCitiesInDbAsync();
    public Task<IEnumerable<Country>> GetCountriesInDbAsync();

}