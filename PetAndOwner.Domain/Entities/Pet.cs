using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Domain.Entities;

public class Pet : IEntity
{
    public Guid Id { get; set; }
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name can't be null or whitespace");
            _name = value;
        }
    }

    private int _age;
    public int Age
    {
        get => _age;
        set
        {
            if (value <= 0) throw new ArgumentException("Age can't be less than or equal to zero");
            _age = value;
        }
    }
    public Gender Gender { get; set; }
    public AnimalType AnimalType { get; set; }
    public ICollection<ApplicationUser> Owners { get; set; } = new List<ApplicationUser>();
}