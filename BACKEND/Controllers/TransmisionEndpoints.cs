using backend.Dto;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

public static class TransmisionEndpoints
{
    public static void MapTransmisionEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Transmision");

        group.MapGet("/", TransmisionService.GetAll)
            .WithName("GetAllTransmisions")
            .Produces<List<Transmision>>();

        group.MapGet("/{id:int}", TransmisionService.Find)
            .WithName("GetTransmisionById")
            .Produces<Transmision>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", TransmisionService.Update)
            .WithName("UpdateTransmision")
            .Accepts<TransmisionDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", TransmisionService.Create)
            .WithName("CreateTransmision")
            .Accepts<TransmisionDto>("application/json")
            .Produces<Transmision>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", TransmisionService.Delete)
            .WithName("DeleteTransmision")
            .Produces(StatusCodes.Status404NotFound);
    }
}