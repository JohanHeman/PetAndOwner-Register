using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Enum;

namespace PetAndOwner.Application.Dto;

public class ReturnPetDto
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string? AnimalType { get; set; }
}