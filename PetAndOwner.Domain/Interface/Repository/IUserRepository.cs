using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Domain.Interface.Repository;

public interface IUserRepository
{
    //crud
    public Task UpdateUserAsync(ApplicationUser user);
    public Task<ApplicationUser> GetUserByIdAsync(Guid id);
    public Task<List<ApplicationUser>> GetAllUsersAsync();
}