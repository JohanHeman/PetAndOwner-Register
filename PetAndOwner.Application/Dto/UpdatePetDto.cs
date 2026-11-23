using PetAndOwner.Domain.Enum;

namespace PetAndOwner.Application.Dto;

public class UpdatePetDto
{
    public Guid PetId { get; set; }
    public string? Name { get; set; }
    public int? Age { get; set; }
    public Gender? Gender { get; set; }
    public string? AnimalType { get; set; }
}