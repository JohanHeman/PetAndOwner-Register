using Microsoft.EntityFrameworkCore;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Domain.Interface.Repository;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    private DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public async Task CreateAsync(T tEntity)
    {
        await _dbSet.AddAsync(tEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T tEntity)
    {
        _dbSet.Update(tEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T tEntity)
    {
        _dbSet.Remove(tEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}