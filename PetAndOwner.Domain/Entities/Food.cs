using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Domain.Entities;

public class Food : IEntity
{
    public Guid Id { get; set; }
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name can't be null or whitespace");
        }
    }
    public FoodBrand FoodBrand { get; set; }
    public Guid FoodBrandId { get; set; }
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}