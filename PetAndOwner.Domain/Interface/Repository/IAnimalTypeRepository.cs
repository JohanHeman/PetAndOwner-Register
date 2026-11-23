using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Domain.Interface.Repository;

public interface IAnimalTypeRepository : IBaseRepository<AnimalType>
{
    public Task<AnimalType?> GetByNameAsync(string name);
}