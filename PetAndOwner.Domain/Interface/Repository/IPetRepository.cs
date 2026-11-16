using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Domain.Interface.Repository;

public interface IPetRepository : IBaseRepository<Pet>
{
    public Task<List<Pet>> GetPetsByUserIdAsync(Guid userId);
}