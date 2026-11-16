using Microsoft.EntityFrameworkCore;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class PetRepository : IPetRepository
{
    private readonly ApplicationDbContext _context;
    public PetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreatePetAsync(Pet pet)
    {
        await _context.Pets.AddAsync(pet);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePetAsync(Pet pet)
    {
        _context.Pets.Update(pet);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePetAsync(Pet pet)
    {
        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
    }

    public async Task<Pet?> GetPetByIdAsync(Guid id)
    {
        return await _context.Pets.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pet>> GetPetsAsync()
    {
        return await _context.Pets.AsNoTracking().ToListAsync();
    }

    public async Task<List<Pet>> GetPetsByUserIdAsync(Guid userId)
    {
        return await _context.Pets
            .AsNoTracking()
            .Where(p => p.Users.Any(u => u.Id == userId))
            .ToListAsync();
    }
}