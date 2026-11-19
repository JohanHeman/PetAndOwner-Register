using Microsoft.EntityFrameworkCore;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class PetRepository : BaseRepository<Pet>, IPetRepository
{
    public PetRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Pet>> GetPetsByUserIdAsync(Guid userId)
    {
        return await _context.Pets
            .AsNoTracking()
            .Where(p => p.Owners.Any(u => u.Id == userId))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ApplicationUser>> GetOwnersByPetIdAsync(Guid petId)
    {
        return await _context.Users.Where(u => u.Pets.Any(p => p.Id == petId)).ToListAsync();
    }

    public async Task RemoveOwnerAsync(Guid petId, Guid ownerId)
    {
        var pet = await _context.Pets.Include(p => p.Owners).FirstOrDefaultAsync(p => p.Id == petId);

        var owner = _context.Users.FirstOrDefault(u => u.Id == ownerId);

        if (owner != null)
        {
            pet.Owners.Remove(owner);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddOwnerAsync(Guid petId, Guid ownerId)
    {
        var pet = await _context.Pets.Include(p => p.Owners).FirstOrDefaultAsync(p => p.Id == petId);

        var owner = _context.Users.FirstOrDefault(u => u.Id == ownerId);

        if (owner != null)
        {
            pet.Owners.Add(owner);
            await _context.SaveChangesAsync();
        }
    }
}