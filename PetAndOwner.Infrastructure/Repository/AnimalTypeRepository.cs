using Microsoft.EntityFrameworkCore;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class AnimalTypeRepository : BaseRepository<AnimalType>, IAnimalTypeRepository
{
    public AnimalTypeRepository(ApplicationDbContext context) : base(context) { }
    
    public async Task<AnimalType?> GetByNameAsync(string name)
    {
        return await _context.AnimalTypes.FirstOrDefaultAsync(c => c.Name == name);
    }
}