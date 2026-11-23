using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PetAndOwner.Application.Dto;
using PetAndOwner.Application.Interface;
using PetAndOwner.Application.Service;

namespace PetAndOwner.Api;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/persons", async ([FromServices] IUserService userService) => await userService.GetAllUsersAsync());

        app.MapGet("/api/profile", async (ClaimsPrincipal claims, [FromServices] IUserService userService) =>
        {
            var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var user = await userService.GetUserByIdAsync(Guid.Parse(userId));
                return Results.Ok(user);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });

        app.MapPatch("/api/profile/setup", async (ClaimsPrincipal claims, [FromServices] IUserService userService, [FromBody] SetupProfileDto profileDto, [FromServices] ILocationService locationService) =>
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


        app.MapDelete("/api/profile", async (ClaimsPrincipal claims, [FromServices] IUserService userService) =>
        {
            var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Results.Unauthorized();

            await userService.DeleteUserAsync(Guid.Parse(userId));

            return Results.NoContent();
        });

        app.MapPatch("/api/profile/update", async (ClaimsPrincipal Claims, [FromServices] IUserService userService, UserProfileDto profileDto, [FromServices] ILocationService locationService) =>
        {
            var userId = Claims.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await userService.GetUserByIdAsync(Guid.Parse(userId));

            var city = await locationService.GetOrSaveCityAsync(profileDto.CityName);

            await userService.UpdateUserAsync(profileDto, user, city);
        });
    }
}