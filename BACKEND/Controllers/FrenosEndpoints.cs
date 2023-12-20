using backend.Dto;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

public static class FrenosEndpoints
{
    public static void MapFrenosEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Frenos").RequireAuthorization();

        group.MapGet("/", FrenosService.GetAll)
            .WithName("GetAllFrenos")
            .Produces<List<Frenos>>();

        group.MapGet("/{id:int}", FrenosService.Find)
            .WithName("GetFrenosById")
            .Produces<Frenos>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", FrenosService.Update)
            .WithName("UpdateFrenos")
            .Accepts<FrenosDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", FrenosService.Create)
            .WithName("CreateFrenos")
            .Accepts<FrenosDto>("application/json")
            .Produces<Frenos>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", FrenosService.Delete)
            .WithName("DeleteFrenos")
            .Produces(StatusCodes.Status404NotFound);
    }
}