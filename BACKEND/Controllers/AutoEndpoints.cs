using backend.Dto;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

public static class AutoEndpoints
{
    public static void MapAutoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Auto").RequireAuthorization();

        group.MapGet("/", AutoService.GetAll)
            .WithName("GetAllAutos")
            .Produces<List<Auto>>();

        group.MapGet("/{id:int}", AutoService.Find)
            .WithName("GetAutoById")
            .Produces<Auto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", AutoService.Update)
            .WithName("UpdateAuto")
            .Accepts<AutoDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", AutoService.Create)
            .WithName("CreateAuto")
            .Accepts<AutoDto>("application/json")
            .Produces<Auto>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", AutoService.Delete)
            .WithName("DeleteAuto")
            .Produces(StatusCodes.Status404NotFound);
    }
}