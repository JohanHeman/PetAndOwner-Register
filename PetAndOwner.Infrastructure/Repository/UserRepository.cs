using Microsoft.AspNetCore.Identity;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task UpdateUserAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("An error occured while updating gthe user.");
        }
    }

    public Task GetUserByIdAsync(Guid id)
    {
        return _userManager.FindByIdAsync(id.ToString());
    }
}