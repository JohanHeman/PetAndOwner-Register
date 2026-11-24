using Microsoft.AspNetCore.Identity;
using PetAndOwner.Domain.Enum;

namespace PetAndOwner.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    private string _name;
    public string? Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 30) throw new ArgumentException("The name must be between 1 and 30 characters");
            _name = value;
        }
    }

    public DateOnly? BirthDate { get; set; }
    public int? Age
    {
        get
        {
            if (BirthDate is null) return null;

            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - BirthDate.Value.Year;

            // Subtract one year if this year's birthday hasn't happened yet
            if (BirthDate.Value > today.AddYears(-age)) age--;
            return age;
        }
    }

    public string? PhoneNumber { get; set; }
    public Guid? CityId { get; set; }
    public City? City { get; set; }
    public Gender? Gender { get; set; }
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}