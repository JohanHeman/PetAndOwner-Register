using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Application.Interface;

public interface ILocationService
{
    public Task<City> GetOrSaveCityAsync(string cityName, string countryName);
    public Task<Country> GetOrSaveCountryAsync(string countryName);

    public Task<IReadOnlyList<City>> GetCitiesInDbAsync();
    public Task<IReadOnlyList<Country>> GetCountriesInDbAsync();

}