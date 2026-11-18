using Microsoft.EntityFrameworkCore;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext context) : base(context) { }
    public async Task<City?> GetCityByNameAsync(string name)
    {
        return await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
    }
}