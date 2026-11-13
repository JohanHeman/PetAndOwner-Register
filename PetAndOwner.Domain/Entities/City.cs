namespace PetAndOwner.Domain.Entities;

public class City
{
    public Guid Id{ get; set; }
    public string Name{ get; set; }
    public Guid CountryId{ get; set; }
    // add ICollection of users 
}