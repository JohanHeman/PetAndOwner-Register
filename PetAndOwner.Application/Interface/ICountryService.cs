using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Application.Interface;

public interface ICountryService
{
    public Task DeleteCountryAsync(Guid id);
    public Task<IEnumerable<Country>> GetAllCountriesAsync();
    public Task<Country?> GetCountryAsync(Guid id);
}