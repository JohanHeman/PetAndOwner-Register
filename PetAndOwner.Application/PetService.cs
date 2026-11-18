using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Application;

public class PetService : IPetService
{
    private readonly IPetRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;

    public PetService(IPetRepository repository, UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
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

    public async Task RemoveOwnerAsync(Guid petId, Guid ownerId)
    {
        var owner = await _userManager.FindByIdAsync(ownerId.ToString());
        if (owner == null) throw new ArgumentException("Owner not found");
        var pet = await _repository.GetByIdAsync(petId);
        if (pet == null) throw new ArgumentException("Pet not found");
        await _repository.RemoveOwnerAsync(petId, ownerId);
    }

    public async Task AddOwnerAsync(Guid petId, Guid newOwnerId)
    {
        var owner = await _userManager.FindByIdAsync(newOwnerId.ToString());
        if (owner == null) throw new ArgumentException("Owner not found");
        var pet = await _repository.GetByIdAsync(petId);
        if (pet == null) throw new ArgumentException("Pet not found");
        await _repository.AddOwnerAsync(petId, newOwnerId);
    }
}