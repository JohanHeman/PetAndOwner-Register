using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Domain.Interface.Repository;

public interface ICountryRepository : IBaseRepository<Country>
{
    public Task<Country?> GetCountryByNameAsync(string name);
}