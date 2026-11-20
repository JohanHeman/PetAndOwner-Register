
namespace PetAndOwner.Application.Dto;

public class SetupProfileDto
{
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string CityName { get; set; }
}