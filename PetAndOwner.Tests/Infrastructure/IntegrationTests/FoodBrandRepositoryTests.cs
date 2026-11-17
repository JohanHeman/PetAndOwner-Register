using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Tests.Infrastructure.IntegrationTests;

public class FoodBrandRepositoryTests : BaseRepositoryTests<FoodBrand>
{
    protected override FoodBrand CreateEntity()
    {
        return new FoodBrand()
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };
    }
}