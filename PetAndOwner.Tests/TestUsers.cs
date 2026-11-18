using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Tests;

public static class TestUsers
{
    public static ApplicationUser Nisse() => CreateTestUser(
        name: "Nisse Svensson", age: 22, email: "Nisse@example.com", cityName: "Stockholm");

    public static ApplicationUser Sigrid() => CreateTestUser(
        name: "Sigrid Sten", age: 52, email: "Sigrid@example.com", cityName: "Gothenburg");

    public static ApplicationUser Barbro() => CreateTestUser(
        name: "Barbro BroBerg", age: 30, email: "Barbro@example.com", cityName: "Malmö");

    public static ApplicationUser BadUser() => CreateTestUser(
        name: "Bad User", age: 50000, email: "mybadmail.com", cityName: "noooooooooo");

    public static ApplicationUser CreateTestUser(
        string name = "Test User",
        int age = 30,
        string email = "test@example.com",
        string cityName = "TestCity",
        string countryName = "TestCountry",
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
            Gender = default//
        };
    }
}