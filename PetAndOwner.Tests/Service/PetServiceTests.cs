using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using PetAndOwner.Application;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Tests.Service;

public class PetServiceTests
{
    private readonly Mock<IPetRepository> _repository;
    private readonly PetService _sut;
    private readonly Mock<UserManager<ApplicationUser>> _userManager;
    public PetServiceTests()
    {
        _repository = new Mock<IPetRepository>();
        var store = new Mock<IUserStore<ApplicationUser>>();

        _userManager = MockUserManager.Create();
        _sut = new PetService(_repository.Object, _userManager.Object);
    }

    [Fact]
    public async Task RegisterPetAsync_ShouldCreatePet()
    {
        // Arrange
        var pet = new Pet()
        {
            Id = Guid.NewGuid(),
            Age = 4
        };

        // Act
        await _sut.RegisterPetAsync(pet);

        // Assert 
        _repository.Verify(x => x.CreateAsync(pet), Times.Once);
    }

    [Fact]
    public async Task UpdatePetAsync_ShouldUpdatePet()
    {
        // Arrange
        var pet = new Pet();

        // Act
        await _sut.UpdatePetAsync(pet);

        // Assert
        _repository.Verify(x => x.UpdateAsync(pet), Times.Once);
    }

    [Fact]
    public async Task DeletePetAsync_ShouldDeletePet()
    {
        // Arrange
        var pet = new Pet();
        _repository.Setup(x => x.GetByIdAsync(pet.Id)).ReturnsAsync(pet);
        // Act
        await _sut.RemovePetAsync(pet.Id);

        // Assert
        _repository.Verify(x => x.DeleteAsync(pet), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowExceptionIfPetDoesntExist()
    {
        // Arrange
        var pet = new Pet();
        var expected = "The pet does not exist";
        // Act
        var actual = Assert.ThrowsAsync<Exception>(() => _sut.RemovePetAsync(pet.Id));

        // Assert
        _repository.Verify(x => x.DeleteAsync(pet), Times.Never);
    }

    [Fact]
    public async Task GetAllPetsASinglePet_ShouldReturnAllPets()
    {
        // Arrange
        var petOne = new Pet();
        var petTwo = new Pet();
        var petThree = new Pet();
        var pets = new List<Pet>() { petOne, petTwo, petThree };
        _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(pets);

        var expected = pets.Count;
        // Act
        var actual = await _sut.GetAllPetsAsync();

        // Assert
        Assert.Equal(expected, actual.Count());
    }

    [Fact]
    public async Task GetPetByIdAsync_ShouldReturnPet()
    {
        // Arrange
        var pet = new Pet()
        {
            Id = Guid.NewGuid(),
        };

        _repository.Setup(x => x.GetByIdAsync(pet.Id)).ReturnsAsync(pet);

        // Act
        var actual = await _sut.GetPetByIdAsync(pet.Id);
        // Assert
        _repository.Verify(x => x.GetByIdAsync(pet.Id), Times.Once);
        Assert.Equal(pet, actual);
    }

    [Fact]
    public async Task GetOwnersByPetIdAsync_ShouldReturnCorrectOwners()
    {
        // Arrange
        var pet = new Pet() { Id = Guid.NewGuid() };
        var owner = new ApplicationUser();
        var owners = new List<ApplicationUser> { owner };

        _repository.Setup(x => x.GetOwnersByPetIdAsync(pet.Id)).ReturnsAsync(owners);

        // Act
        var actual = await _sut.GetOwnersByPetIdAsync(pet.Id);

        // Assert
        Assert.Equal(owners, actual);
        _repository.Verify(x => x.GetOwnersByPetIdAsync(pet.Id), Times.Once);
    }

    [Fact]
    public async Task RemoveOwnerAsync_ShouldRemoveOwner_WhenPetAndOwnerExist()
    {
        // Arrange
        var owner = TestUsers.CreateTestUser();

        var pet = new Pet()
        {
            Id = Guid.NewGuid(),
        };

        _userManager.Setup(x => x.FindByIdAsync(owner.Id.ToString())).ReturnsAsync(owner);
        _repository.Setup(x => x.GetByIdAsync(pet.Id)).ReturnsAsync(pet);

        // Act

        await _sut.RemoveOwnerAsync(pet.Id, owner.Id);

        // Assert
        _repository.Verify(x => x.RemoveOwnerAsync(pet.Id, owner.Id), Times.Once);
    }
}