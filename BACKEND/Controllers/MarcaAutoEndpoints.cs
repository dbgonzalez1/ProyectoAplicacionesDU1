using backend.Dto;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

public static class MarcaAutoEndpoints
{
    public static void MapMarcaAutoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/MarcaAuto").RequireAuthorization();

        group.MapGet("/", MarcaAutoService.GetAll)
            .WithName("GetAllMarcaAutos")
            .Produces<List<MarcaAuto>>();

        group.MapGet("/{id:int}", MarcaAutoService.Find)
            .WithName("GetMarcaAutoById")
            .Produces<MarcaAuto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", MarcaAutoService.Update)
            .WithName("UpdateMarcaAuto")
            .Accepts<MarcaAutoDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", MarcaAutoService.Create)
            .WithName("CreateMarcaAuto")
            .Accepts<MarcaAutoDto>("application/json")
            .Produces<MarcaAuto>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", MarcaAutoService.Delete)
            .WithName("DeleteMarcaAuto")
            .Produces(StatusCodes.Status404NotFound);
    }
}