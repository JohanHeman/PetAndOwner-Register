using Microsoft.AspNetCore.Identity;
using Moq;
using PetAndOwner.Application.Service;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Enum;
using PetAndOwner.Domain.Interface.Repository;

namespace PetAndOwner.Tests.Service;

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
    public async Task UpdateUserAsync_ValidUser_CopiesAllowedFieldsAndSavesStoredUser()
    {
        //arrange
        var userInDb = TestUsers.Barbro();
        var userUpdate = TestUsers.CreateTestUser(
            id: userInDb.Id,
            name: "new name",
            age: 40,
            gender: Gender.Male,
            cityName: "new city");

        _userManager.Setup(m => m.FindByIdAsync(userInDb.Id.ToString())).ReturnsAsync(userInDb);
        _userManager.Setup(m => m.UpdateAsync(userInDb)).ReturnsAsync(IdentityResult.Success);

        //act
        await _sut.UpdateUserAsync(userUpdate);

        //assert
        Assert.Equal("new name", userInDb.Name);
        Assert.Equal(40, userInDb.Age);
        Assert.Equal(Gender.Male, userInDb.Gender);
        Assert.Equal(userUpdate.CityId, userInDb.CityId);
        _userManager.Verify(m => m.UpdateAsync(userInDb), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_UserNotFound_ThrowsExceptionAndDoesNotUpdate()
    {
        //arrange
        var wrongId = Guid.Empty;
        var userUpdate = TestUsers.CreateTestUser(id: wrongId, name: "new name");

        _userManager.Setup(m => m.FindByIdAsync(wrongId.ToString())).ReturnsAsync((ApplicationUser?)null);

        //act + assert 
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _sut.UpdateUserAsync(userUpdate));
        _userManager.Verify(m => m.UpdateAsync(userUpdate), Times.Never);
    }

    [Fact]
    public async Task DeleteUserAsync_IsNotFound_ThrowsException()
    {
        //arrange
        var wrongId = Guid.Empty;
        var userDelete = TestUsers.CreateTestUser(id: wrongId, name: "bad name");

        _userManager.Setup(m => m.FindByIdAsync(wrongId.ToString())).ReturnsAsync((ApplicationUser?)null);

        //act + assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _sut.DeleteUserAsync(userDelete.Id));
        _userManager.Verify(m => m.DeleteAsync(userDelete), Times.Never);
    }

    [Fact]
    public async Task GetUserPetsAsync_ReturnsUserPets()
    {
        //arrange
        var pets = new List<Pet>()
        {
            new Pet { Name = "Pepperoni", Age = 4, Gender = Gender.Female, AnimalType = new AnimalType { Name = "cat" } },
            new Pet { Name = "Tangerine", Age = 2, Gender = Gender.Male, AnimalType = new AnimalType { Name = "cat" } }
        };
        var user = TestUsers.Nisse();
        user.Pets = pets;
        _userManager.Setup(m => m.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);
        _petRepo.Setup(r => r.GetPetsByUserIdAsync(user.Id)).ReturnsAsync((List<Pet>)user.Pets);

        //act
        await _sut.GetUserPetsAsync(user.Id);

        //assert
        _userManager.Verify(m => m.FindByIdAsync(user.Id.ToString()), Times.Once);
        _petRepo.Verify(r => r.GetPetsByUserIdAsync(user.Id), Times.Once);
        Assert.Equal(user.Pets, pets);
    }
}