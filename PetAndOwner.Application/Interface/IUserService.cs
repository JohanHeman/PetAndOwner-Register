using PetAndOwner.Application.Dto;
using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Application.Interface;

public interface IUserService
{
    public Task DeleteUserAsync(Guid userId);
    public Task UpdateUserAsync(UpdateUserProfileDto profileDto, ApplicationUser user, City city);
    public Task<ProfileStatusDto> GetUserProfileStatusByIdAsync(Guid id);
    public Task<ApplicationUser> GetUserByIdAsync(Guid userId);
    public Task<List<ReturnUserProfileDto>> GetAllUsersAsync();
    Task<IReadOnlyList<ReturnPetDto>> GetUserPetsAsync(Guid userId);//UserPetsResponseDto instead of return List<Pet> maybe
    public Task<bool> SetupUserProfileAsync(string userId, UpdateUserProfileDto profileDto, City city);

}