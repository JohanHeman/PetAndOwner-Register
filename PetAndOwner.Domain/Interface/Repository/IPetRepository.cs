using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Domain.Interface.Repository;

public interface IPetRepository
{
    public Task CreatePetAsync(Pet pet);
    public Task UpdatePetAsync(Pet pet);
    public Task DeletePetAsync(Pet pet);
    public Task<Pet?> GetPetByIdAsync(Guid id);
    public Task<List<Pet>> GetPetsAsync();
    public Task<List<Pet>> GetPetsByUserIdAsync(Guid userId);
}