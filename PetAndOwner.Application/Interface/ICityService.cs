using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Application.Interface;

public interface ICityService
{
    public Task DeleteCityAsync(Guid id);
    public Task<IEnumerable<City>> GetAllCitiesAsync();
    public Task<City> GetCityAsync(Guid id);
}