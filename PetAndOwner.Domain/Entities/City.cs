namespace PetAndOwner.Domain.Entities;

public class City : IEntity
{
    public Guid Id { get; set; }
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name can't be null or whitespace.");
            _name = value;
        }
    }

    public Guid CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}