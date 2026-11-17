using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Application;

public class PetService : IPetService
{
    private readonly IPetRepository _repository;

    public PetService(IPetRepository repository)
    {
        _repository = repository;
    }

    public async Task RegisterPetAsync(Pet pet)
    {
        await _repository.CreateAsync(pet);
    }

    public async Task UpdatePetAsync(Pet pet)
    {
        await _repository.UpdateAsync(pet);
    }

    public async Task RemovePetAsync(Guid petId)
    {
        var pet = await _repository.GetByIdAsync(petId);
        if (pet == null) throw new Exception("The pet does not exist");
        await _repository.DeleteAsync(pet);
    }

    public Task<List<Pet>> GetAllPetsAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Pet> GetPetByIdAsync(Guid id)
    {
        return _repository.GetByIdAsync(id);
    }

    public async Task<List<ApplicationUser>> GetOwnersByPetIdAsync(Guid petId)
    {
        return await _repository.GetOwnersByPetIdAsync(petId);
    }

    public Task RemoveOwnerAsync(Guid petId, Guid ownerId)
    {
        throw new NotImplementedException();
    }

    public Task AddOwnersAsync(Guid petId, Guid newOwnerId)
    {
        throw new NotImplementedException();
    }
}