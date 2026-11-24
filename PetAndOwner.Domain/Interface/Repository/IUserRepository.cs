using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Domain.Interface.Repository;

public interface IUserRepository : IBaseRepository<ApplicationUser>
{
    Task<ApplicationUser> GetByIdIncludingProperties(Guid id);

}