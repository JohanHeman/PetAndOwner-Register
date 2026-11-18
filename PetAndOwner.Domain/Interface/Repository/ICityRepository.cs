using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Domain.Interface.Repository;

public interface ICityRepository : IBaseRepository<City>
{
    public Task<City?> GetCityByNameAsync(string name);
}