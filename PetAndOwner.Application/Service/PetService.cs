using Microsoft.AspNetCore.Identity;
using PetAndOwner.Application.Dto;
using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;

namespace PetAndOwner.Application.Service;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IAnimalTypeRepository _typeRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public PetService(IPetRepository petRepository, IAnimalTypeRepository typeRepository, UserManager<ApplicationUser> userManager)
    {
        _petRepository = petRepository;
        _userManager = userManager;
        _typeRepository = typeRepository;
    }

    public async Task RegisterPetAsync(ReturnPetDto pet, ApplicationUser user)
    {
        if (user == null) throw new ArgumentException("Owner not found");

        var newPet = new Pet
        {
            Id = Guid.NewGuid(),
            Name = pet.Name,
            Gender = pet.Gender,
            Age = pet.Age,
            AnimalType = new AnimalType() { Id = Guid.NewGuid(), Name = pet.AnimalType }
        };
        await _petRepository.CreateAsync(newPet);

        await _petRepository.AddOwnerAsync(newPet.Id, user.Id);
    }


    public async Task UpdatePetAsync(UpdatePetDto dto, Guid userId)
    {
        var pet = await _petRepository.GetWithOwnerByIdAsync(dto.PetId) ?? throw new Exception("Pet not found.");

        if (!pet.Owners.Any(x => x.Id == userId))
        {
            throw new ArgumentException("User is not owner of pet");
        }

        var type = await GetOrCreateTypeAsync(dto.AnimalType);
        pet.Name = dto.Name ?? pet.Name;
        pet.Age = dto.Age ?? pet.Age;
        pet.Gender = dto.Gender ?? pet.Gender;
        pet.AnimalType = type;

        await _petRepository.UpdateAsync(pet);
        Console.WriteLine("");
    }

    private async Task<AnimalType> GetOrCreateTypeAsync(string typeName)
    {
        var type = await _typeRepository.GetByNameAsync(typeName);
        if ( type == null)
        {
            await _typeRepository.CreateAsync(new AnimalType() { Id = Guid.NewGuid(), Name = typeName });
            return await _typeRepository.GetByNameAsync(typeName);
        }

        return type;
    }
    public async Task RemovePetAsync(Guid petId, Guid userId)
    {
        var pet = await _petRepository.GetByIdAsync(petId);

        if (!pet.Owners.Any(x => x.Id == userId))
        {
            throw new ArgumentException("User is not owner of pet");
        }

        if (pet == null) throw new Exception("The pet does not exist");
        await _petRepository.DeleteAsync(pet);
    }

    public async Task<IReadOnlyList<ReturnPetDto>> GetAllPetsAsync()
    {
        var pets = await _petRepository.GetAllPetsAsync();
        var response = new List<ReturnPetDto>();
        foreach (var pet in pets)
        {
            response.Add(new ReturnPetDto
            {
                PetId = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Gender = pet.Gender,
                AnimalType = pet.AnimalType.Name,
            });
        }

        return response;
    }


    public Task<Pet> GetPetByIdAsync(Guid id)
    {
        return _petRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<ApplicationUser>> GetOwnersByPetIdAsync(Guid petId)
    {
        return await _petRepository.GetOwnersByPetIdAsync(petId);
    }

    public async Task RemoveOwnerAsync(Guid petId, Guid ownerId)
    {
        var owner = await _userManager.FindByIdAsync(ownerId.ToString());
        if (owner == null) throw new ArgumentException("Owner not found");
        var pet = await _petRepository.GetByIdAsync(petId);
        if (pet == null) throw new ArgumentException("Pet not found");
        await _petRepository.RemoveOwnerAsync(petId, ownerId);
    }
}