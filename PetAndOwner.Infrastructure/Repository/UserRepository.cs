using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ApplicationUser> GetByIdIncludingProperties(Guid id)
    {
        return await _context.Users.Include(u => u.City).ThenInclude((c => c.Country))
            .FirstOrDefaultAsync((u => u.Id == id));
    }
}