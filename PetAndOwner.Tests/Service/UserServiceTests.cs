using Microsoft.AspNetCore.Identity;
using Moq;
using PetAndOwner.Application;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Tests.Infrastructure;

public class UserServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManager;
    private readonly Mock<IPetRepository> _petRepo;
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _userManager = MockUserManager.Create();
        _petRepo = new Mock<IPetRepository>();
        _sut = new UserService(_userManager.Object, _petRepo.Object); //instanciate a new UserService for the tests using mock repos
    }

    [Fact]
    public async Task GetUserByIdAsync_UserExists_ReturnsUser()
    {
        //arrange
        var testId = Guid.NewGuid();
        var user = TestUsers.CreateTestUser(id: testId);

        _userManager.Setup(u => u.FindByIdAsync(testId.ToString())).ReturnsAsync(user);

        //act
        var result = await _sut.GetUserByIdAsync(testId);

        //assert
        _userManager.Verify(u => u.FindByIdAsync(testId.ToString()), Times.Once);
        Assert.Equal(testId, result.Id);
    }

    [Fact]
    public async Task GetUserByIdAsync_UserIsNull_ThrowsException()
    {
        // arrange
        var testId = Guid.NewGuid();

        _userManager.Setup(u => u.FindByIdAsync(testId.ToString()))
            .ReturnsAsync((ApplicationUser?)null);

        // act + assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _sut.GetUserByIdAsync(testId));
        _userManager.Verify(u => u.FindByIdAsync(testId.ToString()), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync()//figure out good name
    {
        var user1 = TestUsers.Barbro();
        var user2 = TestUsers.Barbro();
    }
    /*
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
    */
}