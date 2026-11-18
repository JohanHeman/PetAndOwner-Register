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
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManager = new Mock<UserManager<ApplicationUser>>(
            store.Object,
            null!, // IOptions<IdentityOptions>
            null!, // IPasswordHasher<TUser>
            null!, // IEnumerable<IUserValidator<TUser>>
            null!, // IEnumerable<IPasswordValidator<TUser>>
            null!, // ILookupNormalizer
            null!, // IdentityErrorDescriber
            null!, // IServiceProvider
            null!  // ILogger<UserManager<TUser>>
        );
        _petRepo = new  Mock<IPetRepository>();
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
}