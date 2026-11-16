using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class FoodBrandRepository : BaseRepository<FoodBrand>, IFoodBrandRepository
{
    public FoodBrandRepository(ApplicationDbContext context) : base(context){}
}