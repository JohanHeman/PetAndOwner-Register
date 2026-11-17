using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Tests.Infrastructure.IntegrationTests;

public class AnimalTypeRepositoryTests : BaseRepositoryTests<AnimalType>
{
    protected override AnimalType CreateEntity()
    {
        return new AnimalType()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
        };
    }
}