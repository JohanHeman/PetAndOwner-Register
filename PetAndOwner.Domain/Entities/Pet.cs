using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Domain.Entities;

public class Pet
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public AnimalType AnimalType { get; set; }
    public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

}