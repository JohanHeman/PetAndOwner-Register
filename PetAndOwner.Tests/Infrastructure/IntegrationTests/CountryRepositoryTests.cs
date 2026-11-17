using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Tests.Infrastructure.IntegrationTests;

public class CountryRepositoryTests : BaseRepositoryTests<Country>
{
    protected override Country CreateEntity()
    {
        return new Country()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
        };
    }
}