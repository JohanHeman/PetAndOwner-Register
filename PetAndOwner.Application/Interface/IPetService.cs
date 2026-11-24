using PetAndOwner.Application.Dto;
using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Application.Interface;

public interface IPetService
{
    public Task RegisterPetAsync(ReturnPetDto pet, ApplicationUser user);
    public Task UpdatePetAsync(UpdatePetDto dto, Guid userId);
    public Task RemovePetAsync(Guid petId, Guid userId);
    public Task<IReadOnlyList<ReturnPetDto>> GetAllPetsAsync();
    public Task<Pet> GetPetByIdAsync(Guid id);
    public Task<IReadOnlyList<ApplicationUser>> GetOwnersByPetIdAsync(Guid petId);
    public Task RemoveOwnerAsync(Guid petId, Guid ownerId);
}