using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Domain.Interface.Repository;

public interface IPetRepository
{
    public Task CreatePetAsync(Pet pet);
    public Task UpdatePetAsync(Pet pet);
    public Task DeletePetAsync(Pet pet);
    public Task<Pet> GetPetByIdAsync(Guid id);
    public Task<ICollection<Pet>> GetPetsAsync();
    public Task<ICollection<Pet>> GetPetsByUserIdAsync(Guid userId);
}