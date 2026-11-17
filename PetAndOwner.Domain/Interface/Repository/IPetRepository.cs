using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Domain.Interface.Repository;

public interface IPetRepository : IBaseRepository<Pet>
{
    public Task<List<Pet>> GetPetsByUserIdAsync(Guid userId);
    public Task<List<ApplicationUser>> GetOwnersByPetIdAsync(Guid petId);
}