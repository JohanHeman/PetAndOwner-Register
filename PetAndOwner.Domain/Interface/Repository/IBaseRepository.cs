namespace PetAndOwner.Domain.Interface.Repository;

public interface IBaseRepository<T> where T : class
{
    public Task CreateAsync(T tEntity);
    public Task UpdateAsync(T tEntity);
    public Task DeleteAsync(T tEntity);
    public Task<T?> GetByIdAsync(Guid id);
    public Task<List<T>> GetAllAsync();
}