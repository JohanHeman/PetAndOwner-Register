using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetAndOwner.Api.Dto;
using PetAndOwner.Application;
using PetAndOwner.Application.Api;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Infrastructure.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://countriesnow.space/api/v0.1");
});


builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}



app.UseHttpsRedirection();
app.MapIdentityApi<ApplicationUser>();


app.MapPatch("/api/profile", async (ClaimsPrincipal claims , UserManager<ApplicationUser> manager, SetupProfileDto profileDto, LocationService locationService) =>
{
    var user = await manager.GetUserAsync(claims);
    
    if (user == null)
    {
        return Results.Unauthorized();
    }
     
    try
    {
        var city = await locationService.GetOrSaveCityAsync(profileDto.CityName);
    
        user.Name = profileDto.Name;
        user.Gender = (Gender) profileDto.Gender;
        user.PhoneNumber = profileDto.PhoneNumber;
        user.Age = profileDto.Age;
        user.City = city;
    
        await manager.UpdateAsync(user);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
    
    return Results.NoContent();
});



app.Run();


