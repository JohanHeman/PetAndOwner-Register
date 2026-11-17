using PetAndOwner.Domain.Entities;

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
}