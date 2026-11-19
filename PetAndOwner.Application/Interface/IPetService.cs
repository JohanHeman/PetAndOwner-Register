using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Application.Interface;

public interface IPetService
{
    public Task RegisterPetAsync(Pet pet);//PetDto dto
    public Task UpdatePetAsync(Pet pet);//PetDto dto
    public Task RemovePetAsync(Guid petId);
    public Task<IReadOnlyList<Pet>> GetAllPetsAsync();
    public Task<Pet> GetPetByIdAsync(Guid id);
    public Task<IReadOnlyList<ApplicationUser>> GetOwnersByPetIdAsync(Guid petId);
    public Task RemoveOwnerAsync(Guid petId, Guid ownerId);
    public Task AddOwnerAsync(Guid petId, Guid newOwnerId);
}