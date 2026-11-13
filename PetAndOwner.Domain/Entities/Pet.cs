namespace PetAndOwner.Domain.Entities;

public class Pet
{
   public Guid Id { get; set; }
   public AnimalType AnimalType{ get; set; }
   public string Name { get; set; }
   public int Age{ get; set; }
   public Gender Gender{ get; set; }
   public ICollection<Allergy> Allergies{ get; set; } =  new List<Allergy>();
   // add applicationuser realationship here 
}