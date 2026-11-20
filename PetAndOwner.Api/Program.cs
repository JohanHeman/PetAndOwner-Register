using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAndOwner.Application;
using PetAndOwner.Application.Api;
using PetAndOwner.Application.Dto;
using PetAndOwner.Application.Interface;
using PetAndOwner.Application.Service;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;
using PetAndOwner.Infrastructure.Repository;
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

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetService, PetService>();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}



app.UseHttpsRedirection();
app.MapIdentityApi<ApplicationUser>();


app.MapPatch("/api/profile", async (ClaimsPrincipal claims, [FromServices] IUserService userService, [FromBody] SetupProfileDto profileDto, [FromServices] ILocationService locationService) =>
{
    var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

    var city = await locationService.GetOrSaveCityAsync(profileDto.CityName);

    try
    {
        await userService.SetupUserProfileAsync(userId, profileDto, city);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }

    return Results.NoContent();
});


app.MapGet("/api/persons", async ([FromServices] IUserService userService) => await userService.GetAllUsersAsync());


app.MapGet("/api/profile", async (ClaimsPrincipal claims, [FromServices] IUserService userService) =>
{
    var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
    if (!Guid.TryParse(userId, out var id))
        return Results.Unauthorized();

    try
    {
        var user = await userService.GetUserByIdAsync(id);
        return Results.Ok(user);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapPost("/api/profile/pet", async (ClaimsPrincipal claims, UserManager<ApplicationUser> manager, [FromServices] IPetService petService, [FromBody] ReturnPetDto pet) =>
{
    var user = await manager.GetUserAsync(claims);

    await petService.RegisterPetAsync(pet, user);
});



app.Run();


