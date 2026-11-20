using Microsoft.AspNetCore.Identity;
using Moq;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Tests;

public static class MockUserManager
{
    public static Mock<UserManager<ApplicationUser>> Create()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();

        return new Mock<UserManager<ApplicationUser>>(
            store.Object,
            null!, // IOptions<IdentityOptions>
            null!, // IPasswordHasher<TUser>
            null!, // IEnumerable<IUserValidator<TUser>>
            null!, // IEnumerable<IPasswordValidator<TUser>>
            null!, // ILookupNormalizer
            null!, // IdentityErrorDescriber
            null!, // IServiceProvider
            null!  // ILogger<UserManager<TUser>>
        );
    }
}