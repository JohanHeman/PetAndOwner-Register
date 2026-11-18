using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Tests;

public static class TestUsers
{
    public static ApplicationUser Nisse() => CreateTestUser(
        name: "Nisse Svensson", age: 22, gender: Gender.Male ,email: "Nisse@example.com", cityName: "Stockholm",
        pets: new List<Pet>() { 
            new Pet { Name = "Pepperoni", Age = 4, Gender = Gender.Female, AnimalType = new AnimalType { Name = "cat" }}});

    public static ApplicationUser Sigrid() => CreateTestUser(
        name: "Sigrid Sten", age: 52, gender: Gender.Female, email: "Sigrid@example.com", cityName: "Gothenburg",
        pets: new List<Pet>() {
            new Pet() { Name = "The Duke", Age = 1, Gender = Gender.Male, AnimalType = new AnimalType { Name = "Duck" }}});

    public static ApplicationUser Barbro() => CreateTestUser(
        name: "Barbro BroBerg", age: 30, gender: Gender.Female, email: "Barbro@example.com", cityName: "Malmö");

    public static ApplicationUser BadUser() => CreateTestUser(
        name: "Bad User", age: 50000, gender: Gender.Female, email: "mybadmail.com", cityName: "noooooooooo");
    
    public static ApplicationUser CreateTestUser(
        string name = "Test User",
        int age = 30,
        Gender gender = Gender.Unknown,
        string email = "test@example.com",
        string cityName = "TestCity",
        string countryName = "TestCountry",
        List<Pet> pets = null,
        Guid? id = null)
    {
        var city = new City { Id = Guid.NewGuid(), Name = cityName };
        var country = new Country { Id = Guid.NewGuid(), Name = countryName };

        return new ApplicationUser
        {
            Id = id ?? Guid.NewGuid(),
            UserName = email,
            Email = email,
            Name = name,
            Age = age,
            City = city,
            CityId = city.Id,
            Gender = gender,
            Pets = pets ?? new List<Pet>(),
        };
    }
}