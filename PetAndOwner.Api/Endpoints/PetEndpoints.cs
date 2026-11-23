using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetAndOwner.Application.Dto;
using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Enum;

namespace PetAndOwner.Api;

public static class PetEndpoints
{
    public static void MapPetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/profile/pet", async (
            ClaimsPrincipal claims, 
            UserManager<ApplicationUser> manager, 
            [FromServices] IPetService petService, 
            [FromBody] ReturnPetDto pet) =>
        {
            var user = await manager.GetUserAsync(claims);
            await petService.RegisterPetAsync(pet, user);
        });

        app.MapGet("/api/profile/pet", async (
            ClaimsPrincipal claims,
            UserManager<ApplicationUser> manager,
            [FromServicesAttribute] IUserService userService) =>
        {
            var identifier = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(identifier, out var userId))
                return Results.Unauthorized();
            
            try
            {
                var pets = await userService.GetUserPetsAsync(userId);
                var response = new List<ReturnPetDto>();
                foreach (var pet in pets)
                {
                    response.Add(new ReturnPetDto
                    {
                        Name = pet.Name,
                        Age = pet.Age,
                        Gender = pet.Gender,
                        AnimalType = pet.AnimalType.Name,
                    });
                }

                return Results.Ok(response);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}