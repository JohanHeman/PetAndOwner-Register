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
            [FromServices] IUserService userService,
            [FromBody] ReturnPetDto pet) =>
        {
            var identifier = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(identifier, out var id))
                return Results.Unauthorized();

            try
            {
                var user = await userService.GetUserByIdAsync(id);
                await petService.RegisterPetAsync(pet, user);
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });

        app.MapGet("/api/profile/pet", async (
            ClaimsPrincipal claims,
            UserManager<ApplicationUser> manager,
            [FromServicesAttribute] IUserService userService) =>
        {
            var identifier = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(identifier, out var id))
                return Results.Unauthorized();

            try
            {
                var pets = await userService.GetUserPetsAsync(id);
                return Results.Ok(pets);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });

        app.MapGet("/api/pets", async (
            [FromServicesAttribute] IPetService petService) =>
        {
            try
            {
                var pets = await petService.GetAllPetsAsync();
                return Results.Ok(pets);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }).Produces<IReadOnlyList<ReturnPetDto>>(200);

        app.MapPatch("/api/profile/pet/{id}", async (
            ClaimsPrincipal claims,
            [FromServices] IPetService petService,
            [FromBody] UpdatePetDto pet, Guid id) =>
        {
            var identifier = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(identifier, out var userId))
                return Results.Unauthorized();
            try
            {
                await petService.UpdatePetAsync(pet, userId);
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });

        app.MapDelete("/api/profile/pet/{id}", async (
            ClaimsPrincipal claims,
            [FromServices] IPetService petService,
            Guid id) =>
        {
            var identifier = claims.FindFirstValue(ClaimTypes.NameIdentifier);

            var pet = await petService.GetPetByIdAsync(id);

            await petService.RemovePetAsync(pet.Id, Guid.Parse(identifier));
            return Results.Ok();

        });
    }
}