namespace PetAndOwner.Application.Dto;

public class UpdateUserProfileDto
{
    public string Name { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CityName { get; set; }
    public string? Email { get; set; }
    public string? Country { get; set; }
}