using Microsoft.EntityFrameworkCore;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;
using PetAndOwner.Infrastructure.Migrations;
using PetAndOwner.Infrastructure.Repository;

namespace PetAndOwner.Tests.Infrastructure.IntegrationTests;

public class PetRepositoryTests : BaseRepositoryTests<Pet>
{
    protected override Pet CreateEntity()
    {
        return new Pet()
        {
            Id = Guid.NewGuid(),
            Name = "Pet",
            Age = 20,
        };
    }

    private PetRepository _repo;
    public PetRepositoryTests()
    {
        _repo = new PetRepository(_dbContext);
    }

    [Fact]
    public async Task RemoveOwnerAsync_ShouldRemoveOwner_WhenPetAndOwnerExist()
    {
        // Arrange
        var pet = new Pet()
        {
            Id = Guid.NewGuid(),
            Name = "Pet",
            Age = 5,
        };

        var owner = new ApplicationUser()
        {
            Name = "Owner",
            Id = Guid.NewGuid(),
            Age = 20,
        };

        await _sut.CreateAsync(pet);
        owner.Pets.Add(pet);
        await _dbContext.Users.AddAsync(owner);
        await _dbContext.SaveChangesAsync();

        // Act
        await _repo.RemoveOwnerAsync(pet.Id, owner.Id);
        // Assert 

        // Includes the users pets to verify that the pet was removed
        var updatedUser = await _dbContext.Users.Include(u => u.Pets)
            .FirstAsync(u => u.Id == owner.Id);
        Assert.DoesNotContain(updatedUser.Pets, p => p.Id == pet.Id);

    }

    [Fact]
    public async Task AddOwnerAsync_ShouldAddOwner_WhenPetAndOwnerExist()
    {
        // Arrange
        var pet = new Pet()
        {
            Id = Guid.NewGuid(),
            Name = "Pet",
            Age = 5,
        };

        var owner = new ApplicationUser()
        {
            Name = "Owner",
            Id = Guid.NewGuid(),
            Age = 20,
        };

        await _sut.CreateAsync(pet);
        owner.Pets.Add(pet);
        await _dbContext.Users.AddAsync(owner);
        await _dbContext.SaveChangesAsync();
        // Act
        await _repo.AddOwnerAsync(pet.Id, owner.Id);

        // Assert
        var updatedUser = await _dbContext.Users.FirstAsync(u => u.Id == owner.Id);
        Assert.Contains(updatedUser.Pets, p => p.Id == pet.Id);
    }
}