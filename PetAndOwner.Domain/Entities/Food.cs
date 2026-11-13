namespace PetAndOwner.Domain.Entities;

public class Food
{
    public Guid Id{ get; set; }
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    public int Age{ get; set; }
    public FoodBrand FoodBrand{ get; set; }
    public Guid FoodBrandId{ get; set; }
}