namespace PetAndOwner.Domain.Entities;

public class Country : IEntity
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
    public ICollection<City> Cities { get; set; } = new List<City>();
}