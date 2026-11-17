namespace PetAndOwner.Domain.Entities;

public class Allergy : IEntity
{
    public Guid Id { get; set; }
    private string _name { get; set; }
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name can't be null or whitespace.");
            _name = value;
        }
    }

    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}