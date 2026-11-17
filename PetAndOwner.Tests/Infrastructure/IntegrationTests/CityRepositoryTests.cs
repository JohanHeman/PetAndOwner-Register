using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Tests.Infrastructure.IntegrationTests;

public class CityRepositoryTests : BaseRepositoryTests<City>
{
    protected override City CreateEntity()
    {
        return new City()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
        };
    }
}