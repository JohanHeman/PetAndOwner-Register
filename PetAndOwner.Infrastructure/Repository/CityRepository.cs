using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Infrastructure.Repository;

public class CityRepository : BaseRepository<City>, ICityRepository
{
   public CityRepository( ApplicationDbContext context) : base(context){}
}