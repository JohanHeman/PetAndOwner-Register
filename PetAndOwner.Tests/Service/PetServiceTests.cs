using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using PetAndOwner.Application;
using PetAndOwner.Application.Dto;
using PetAndOwner.Application.Service;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Tests.Service;

public class PetServiceTests
{
    private readonly PetService _sut;

    private readonly Mock<UserManager<ApplicationUser>> _userManager;
    private readonly Mock<IPetRepository> _repository;
    private readonly Mock<IAnimalTypeRepository> _typerepository;

    public PetServiceTests()
    {
        _repository = new Mock<IPetRepository>();
        _typerepository = new Mock<IAnimalTypeRepository>();
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManager = MockUserManager.Create();

        _sut = new PetService(_repository.Object, _typerepository.Object, _userManager.Object);
    }

    [Fact]
    public async Task RegisterPetAsync_ShouldCreatePet()
    {
        // Arrange
        var user = TestUsers.CreateTestUser();
        var pet = new ReturnPetDto()
        {
            Name = "PetName",
            Age = 4,
            Gender = default,
            AnimalType = "animal"
        };

        // Act
        await _sut.RegisterPetAsync(pet, user);

        // Assert 
        _repository.Verify(x => x.CreateAsync(It.IsAny<Pet>()), Times.Once);
    }

    [Fact]
    public async Task UpdatePetAsync_ShouldUpdatePet()
    {
        // Arrange
        var petId = Guid.NewGuid();
        var user = TestUsers.CreateTestUser();

        var type = new AnimalType { Name = "chicken" };

        var dto = new UpdatePetDto
        {
            PetId = petId,
            Name = "Rex",
            Age = 5,
            AnimalType = "chicken"
        };

        var existingPet = new Pet
        {
            Id = petId,
            Name = "Buddy",
            Age = 3,
            Owners = new List<ApplicationUser> { user },
        };

        _repository.Setup(x => x.GetWithOwnerByIdAsync(petId)).ReturnsAsync(existingPet);
        _typerepository.Setup(x => x.GetByNameAsync("chicken")).ReturnsAsync(type);
        // Act

        await _sut.UpdatePetAsync(dto, user.Id);

        // Assert
        _repository.Verify(x => x.UpdateAsync(It.Is<Pet>(p =>
            p.Id == petId &&
            p.Name == "Rex" &&
            p.Age == 5 &&
            p.AnimalType == type)), Times.Once);
    }

    [Fact]
    public async Task DeletePetAsync_ShouldDeletePet()
    {
        // Arrange
        var pet = new Pet();
        var user = TestUsers.CreateTestUser();
        pet.Owners = new List<ApplicationUser> { user };
        _repository.Setup(x => x.GetWithOwnerByIdAsync(pet.Id)).ReturnsAsync(pet);
        // Act
        await _sut.RemovePetAsync(pet.Id, user.Id);

        // Assert
        _repository.Verify(x => x.DeleteAsync(pet), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowExceptionIfPetDoesntExist()
    {
        // Arrange
        var pet = new Pet();
        var expected = "The pet does not exist";
        var user = TestUsers.CreateTestUser();
        pet.Owners = new List<ApplicationUser> { user };
        // Act
        var actual = Assert.ThrowsAsync<Exception>(() => _sut.RemovePetAsync(pet.Id, user.Id));

        // Assert
        _repository.Verify(x => x.DeleteAsync(pet), Times.Never);
    }

    [Fact]
    public async Task GetAllPetsASinglePet_ShouldReturnAllPets()
    {
        // Arrange
        var dogType = new AnimalType { Name = "Dog" };

        var pets = new List<Pet>
        {
            new Pet { Name = "animal1",   Age = 3, AnimalType = dogType },
            new Pet { Name = "animal2",  Age = 5, AnimalType = dogType },
            new Pet { Name = "animal3", Age = 2, AnimalType = dogType },
        };
        _repository.Setup(x => x.GetAllPetsAsync()).ReturnsAsync(pets);

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
    [Fact]
    public async Task RemoveOwnerAsync_ShouldThrowArgumentException_WhenOwnerDoesNotExist()
    {
        // Arrange
        var petId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var expected = "Owner not found";
        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() => _sut.RemoveOwnerAsync(petId, userId));

        // Assert
        _repository.Verify(x => x.RemoveOwnerAsync(petId, userId), Times.Never);
        Assert.Equal(expected, actual.Message);
    }

    [Fact]
    public async Task RemoveOwnerAsync_ShouldThrowArgumentException_WhenPetDoesNotExist()
    {
        // Arrange
        var petId = Guid.NewGuid();
        var applicationUser = TestUsers.CreateTestUser();
        var expected = "Pet not found";
        _userManager.Setup(x => x.FindByIdAsync(applicationUser.Id.ToString())).ReturnsAsync(applicationUser);
        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() => _sut.RemoveOwnerAsync(petId, applicationUser.Id));

        // Assert
        _repository.Verify(x => x.RemoveOwnerAsync(petId, applicationUser.Id), Times.Never);
        Assert.Equal(expected, actual.Message);
    }

}
