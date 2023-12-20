using backend.Services;
using backend.Dto;
using backend.Models;

namespace backend.Controllers;

public static class NeumaticoEndpoints
{
    public static void MapNeumaticoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Neumatico").RequireAuthorization();

        group.MapGet("/", NeumaticoService.GetAll)
            .WithName("GetAllNeumaticos")
            .Produces<List<Neumatico>>();

        group.MapGet("/{id:int}", NeumaticoService.Find)
            .WithName("GetNeumaticoById")
            .Produces<Neumatico>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", NeumaticoService.Update)
            .WithName("UpdateNeumatico")
            .Accepts<NeumaticoDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", NeumaticoService.Create)
            .WithName("CreateNeumatico")
            .Accepts<NeumaticoDto>("application/json")
            .Produces<Neumatico>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", NeumaticoService.Delete)
            .WithName("DeleteNeumatico")
            .Produces(StatusCodes.Status404NotFound);
    }
}