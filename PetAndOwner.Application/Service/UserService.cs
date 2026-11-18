using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Application;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPetRepository _petRepository;
    public UserService(UserManager<ApplicationUser> manager, IPetRepository petRepository)
    {
        _userManager = manager;
        _petRepository = petRepository;
    }

    public async Task<ApplicationUser> GetUserByIdAsync(Guid id)
    {
        return await FindUserOrThrowAsync(id);
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.AsNoTracking().ToListAsync();
    }

    public async Task UpdateUserAsync(ApplicationUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var existing = await FindUserOrThrowAsync(user.Id);

        // only change values that are allowed to change
        existing.Name = user.Name;
        existing.Age = user.Age;
        existing.Gender = user.Gender;
        existing.CityId = user.CityId;

        var result = await _userManager.UpdateAsync(user);
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

    public async Task<List<Pet>> GetUserPetsAsync(Guid userId)
    {
        await FindUserOrThrowAsync(userId);
        return await _petRepository.GetPetsByUserIdAsync(userId);
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