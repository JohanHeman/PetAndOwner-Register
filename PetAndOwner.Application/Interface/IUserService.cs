using PetAndOwner.Api.Dto;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Application.Interface;

public interface IUserService
{
    public Task DeleteUserAsync(Guid userId);
    public Task UpdateUserAsync(ApplicationUser user);
    public Task<ApplicationUser> GetUserByIdAsync(Guid id);
    public Task<List<ApplicationUser>> GetAllUsersAsync();
    Task<List<Pet>> GetUserPetsAsync(Guid userId);//UserPetsResponseDto instead of return List<Pet> maybe
    public Task<bool> SetupUserProfileAsync(string userId, SetupProfileDto profileDto, City city);

}