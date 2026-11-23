using PetAndOwner.Application.Dto;
using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Application.Interface;

public interface IUserService
{
    public Task DeleteUserAsync(Guid userId);
    public Task UpdateUserAsync(UserProfileDto profileDto, ApplicationUser user, City city);
    public Task<ApplicationUser> GetUserByIdAsync(Guid id);
    public Task<List<UserProfileDto>> GetAllUsersAsync();
    Task<IReadOnlyList<ReturnPetDto>> GetUserPetsAsync(Guid userId);//UserPetsResponseDto instead of return List<Pet> maybe
    public Task<bool> SetupUserProfileAsync(string userId, UserProfileDto profileDto, City city);

}