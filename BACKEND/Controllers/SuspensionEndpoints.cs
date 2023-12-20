using backend.Dto;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

public static class SuspensionEndpoints
{
    public static void MapSuspensionEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Suspension");

        group.MapGet("/",  SuspensionService.GetAll)
            .WithName("GetAllSuspensions")
            .Produces<List<Suspension>>();

        group.MapGet("/{id:int}", SuspensionService.Find)
            .WithName("GetSuspensionById")
            .Produces<Suspension>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", SuspensionService.Update)
            .WithName("UpdateSuspension")
            .Accepts<SuspensionDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", SuspensionService.Create)
            .WithName("CreateSuspension")
            .Accepts<SuspensionDto>("application/json")
            .Produces<Suspension>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", SuspensionService.Delete)
            .WithName("DeleteSuspension")
            .Produces(StatusCodes.Status404NotFound);
    }
}