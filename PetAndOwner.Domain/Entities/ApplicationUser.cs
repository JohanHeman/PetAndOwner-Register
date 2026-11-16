using Microsoft.AspNetCore.Identity;
using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Infrastructure.Data;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }
    public Guid CityId { get; set; }
    public City City { get; set; }
    public Gender Gender { get; set; }
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}