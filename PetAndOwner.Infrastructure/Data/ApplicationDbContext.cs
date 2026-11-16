using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Pet> Pets { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodBrand> FoodBrands { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<AnimalType> AnimalTypes { get; set; }
    public DbSet<Allergy> Allergies { get; set; }







}