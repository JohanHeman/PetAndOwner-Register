using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Application.Interface;

public interface IPetService
{
    public Task RegisterPetAsync(Guid ownerId, Pet pet);//PetDto dto
    public Task UpdatePetAsync(Guid petId, Pet pet);//PetDto dto
    public Task RemovePetAsync(Guid petId);
    public Task<List<Pet>> GetAllPetsAsync();
    public Task<Pet> GetPetByIdAsync(Guid id);
    public Task GetOwnersByPetAsync(Guid petId);   
    public Task RemoveOwnerAsync(Guid petId, Guid ownerId);    
    public Task AddOwnersAsync(Guid petId, Guid newOwnerId);
    
}