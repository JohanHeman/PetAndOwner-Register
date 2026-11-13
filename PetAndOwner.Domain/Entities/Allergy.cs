namespace PetAndOwner.Domain.Entities;

public class Allergy
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}