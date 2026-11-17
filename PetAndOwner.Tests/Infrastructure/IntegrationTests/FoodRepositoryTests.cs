using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Tests.Infrastructure.IntegrationTests;

public class FoodRepositoryTests : BaseRepositoryTests<Food>
{
    protected override Food CreateEntity()
    {
        return new Food()
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };
    }
}