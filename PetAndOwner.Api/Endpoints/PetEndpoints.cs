using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetAndOwner.Application.Dto;
using PetAndOwner.Application.Interface;
using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Api;

public static class PetEndpoints
{
    public static void MapPetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/profile/pet", async (ClaimsPrincipal claims, UserManager<ApplicationUser> manager, [FromServices] IPetService petService, [FromBody] ReturnPetDto pet) =>
        {
            var user = await manager.GetUserAsync(claims);

            await petService.RegisterPetAsync(pet, user);
        });
    }
}