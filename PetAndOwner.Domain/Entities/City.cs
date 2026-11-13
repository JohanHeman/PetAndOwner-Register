using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Domain.Entities;

public class City
{
    public Guid Id{ get; set; }
    public string Name{ get; set; }
    public Guid CountryId{ get; set; }
    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}