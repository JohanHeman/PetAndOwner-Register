using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Application.Interface;

public interface IUserService
{
    public Task<ApplicationUser> GetUserAsync(Guid id);
    public Task UpdateUserAsync(ApplicationUser user);
    public Task<List<ApplicationUser>> GetAllUsersAsync();
}