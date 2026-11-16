using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Application.Interface;

public interface IPetService
{
    public Task CreatePetAsync(string name, int age, Gender gender);
    public Task SetPetOwnerAsync(ApplicationUser user, Pet pet);
    public Task UpdatePetAsync(Guid petId);
    public Task DeletePetAsync(Guid petId);
    public Task<List<Pet>> GetPetsAsync();
    public Task<Pet> GetPetByIdAsync(Guid id);
}