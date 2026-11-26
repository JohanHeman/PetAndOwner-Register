using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Domain.Interface.Repository;

public interface IPetRepository : IBaseRepository<Pet>
{
    public Task<List<Pet>> GetPetsByUserIdAsync(Guid userId);
    public Task<IReadOnlyList<ApplicationUser>> GetOwnersByPetIdAsync(Guid petId);
    public Task RemoveOwnerAsync(Guid petId, Guid ownerId);
    public Task AddOwnerAsync(Guid petId, Guid ownerId);
    public Task<List<Pet>> GetAllPetsAsync();

    public Task<Pet?> GetWithOwnerByIdAsync(Guid petId);
}