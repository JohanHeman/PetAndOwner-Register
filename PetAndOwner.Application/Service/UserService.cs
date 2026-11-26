using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetAndOwner.Application.Dto;
using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Enum;
using PetAndOwner.Domain.Interface.Repository;

namespace PetAndOwner.Application.Service;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPetRepository _petRepository;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<ApplicationUser> manager, IPetRepository petRepository, IUserRepository userRepository)
    {
        _userManager = manager;
        _petRepository = petRepository;
        _userRepository = userRepository;
    }

    public async Task<ProfileStatusDto> GetProfileStatusByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdIncludingProperties(id);
        bool profileStatus = false;
        ReturnUserProfileDto dto = new ReturnUserProfileDto()
        {
            Id = user.Id,
            Name = user.Name,
            BirthDate = user.BirthDate,
            Gender = user.Gender?.ToString(),
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CityName = user.City?.Name,
            Country = user.City?.Country?.Name,
            Age = user.Age
        };

        //check for null in all the properties
        bool hasNull = dto.GetType().GetProperties().Any(p => p.GetValue(dto) is null);

        ProfileStatusDto profileStatusDto = new ProfileStatusDto()
        {
            IsCompleted = !hasNull,
            Profile = dto
        };

        return profileStatusDto;
    }

    public async Task<ReturnUserProfileDto> GetUserProfileByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdIncludingProperties(id);

        ReturnUserProfileDto dto = new ReturnUserProfileDto()
        {
            Id = user.Id,
            Name = user.Name,
            Age = user.Age,
            BirthDate = user.BirthDate,
            CityName = user.City?.Name,
            Country = user.City?.Country?.Name,
            Gender = user.Gender?.ToString(),
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
        };

        return dto;
    }

    public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
    {
        return await FindUserOrThrowAsync(userId);
    }

    public async Task<List<ReturnUserProfileDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.Include(u => u.City)
            .ThenInclude(C => C.Country).ToListAsync();
        List<ReturnUserProfileDto> persons = new List<ReturnUserProfileDto>();
        foreach (var user in users)
        {
            if (user.BirthDate is null ||
                user.Age is null ||
                user.City?.Country is null ||
                user.Name is null ||
                user.Email is null ||
                user.PhoneNumber is null) continue;
            
            var person = new ReturnUserProfileDto()
            {
                Name = user.Name,
                BirthDate = user.BirthDate,
                Gender = user.Gender.ToString(),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CityName = user.City.Name,
                Country = user.City.Country.Name,
                Age = user.Age
            };
            
            persons.Add(person);
            
        }
        return persons;
    }

    public async Task<bool> SetupUserProfileAsync(Guid userId, UpdateUserProfileDto profileDto, City city)
    {
        var user = await _userRepository.GetByIdIncludingProperties(userId);

        if (user == null)
        {
            return false;
        }

        user.Name = profileDto.Name;
        user.Gender = Enum.Parse<Gender>(profileDto.Gender);
        user.PhoneNumber = profileDto.PhoneNumber;
        user.BirthDate = profileDto.BirthDate;
        user.City = city;

        await _userManager.UpdateAsync(user);
        return true;
    }

    public async Task UpdateUserAsync(UpdateUserProfileDto profileDto, ApplicationUser user, City city)
    {
        var existing = await FindUserOrThrowAsync(user.Id);

        if (existing == null) throw new Exception("User not found");

        // only change values that are allowed to change
        existing.Name = profileDto.Name;
        existing.BirthDate = profileDto.BirthDate;
        existing.Gender = Enum.Parse<Gender>(profileDto.Gender);
        existing.Email = profileDto.Email;
        existing.PhoneNumber = profileDto.PhoneNumber;
        existing.BirthDate = profileDto.BirthDate;
        existing.City = city;
        existing.CityId = city.Id;
        var result = await _userManager.UpdateAsync(existing);
        ThrowIfFailed(result, "update user");
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await FindUserOrThrowAsync(userId);

        var pets = await _petRepository.GetPetsByUserIdAsync(userId);
        var soleOwned = pets.FirstOrDefault(p => p.Owners.Count == 1);
        if (soleOwned != null)
            throw new InvalidOperationException(
                $"User is the only owner of pet '{soleOwned.Name}'. Transfer or delete pet first.");

        var result = await _userManager.DeleteAsync(user);
        ThrowIfFailed(result, "delete user");
    }

    public async Task<IReadOnlyList<ReturnPetDto>> GetUserPetsAsync(Guid userId)
    {
        await FindUserOrThrowAsync(userId);
        var pets = await _petRepository.GetPetsByUserIdAsync(userId);
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

    // there are several parts that need a check that checks if user exists before trying to take further action
    private async Task<ApplicationUser> FindUserOrThrowAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString()) ?? throw new KeyNotFoundException($"User '{id}' was not found.");
    }

    // UserManager reports failures through IdentityResult instead of throwing, so an
    // unchecked result lets a failed update or delete pass silently as a success.
    // the choice to put it into a method is to avoid repeating code
    private static void ThrowIfFailed(IdentityResult result, string action)
    {
        if (result.Succeeded) return;

        var errors = string.Join("; ", result.Errors.Select(e => e.Description));
        throw new InvalidOperationException($"Failed to {action}: {errors}");
    }
}