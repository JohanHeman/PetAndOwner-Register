using Microsoft.EntityFrameworkCore;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;
using PetAndOwner.Infrastructure.Repository;

namespace PetAndOwner.Tests.Infrastructure.IntegrationTests;

public abstract class BaseRepositoryTests<TEntity> where TEntity : class, IEntity, new()
{
    protected readonly ApplicationDbContext _dbContext;

    protected readonly TestBaseRepository<TEntity> _sut;

    protected abstract TEntity CreateEntity();

    public BaseRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _dbContext = new ApplicationDbContext(options);

        _sut = new TestBaseRepository<TEntity>(_dbContext);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddEntity_WhenValidProperties()
    {
        // Arrange
        TEntity entity = CreateEntity();
        // Act
        await _sut.CreateAsync(entity);
        // Assert
        var result = await _dbContext.Set<TEntity>().FindAsync((entity.Id));

        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity_WhenValidId()
    {
        // Arrange

        TEntity entity = CreateEntity();
        await _sut.CreateAsync(entity);
        // Act
        await _sut.DeleteAsync(entity);

        // Assert
        var result = await _dbContext.Set<TEntity>().FindAsync((entity.Id));
        Assert.Null(result);
    }

}