using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class FoodRepository : BaseRepository<Food>, IFoodRepository
{
    public FoodRepository(ApplicationDbContext context) : base(context) {}
}