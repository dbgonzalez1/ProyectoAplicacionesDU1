using backend.Services;
using backend.Dto;
using backend.Models;

namespace backend.Controllers;

public static class AmortiguadorEndpoints
{
    public static void MapAmortiguadorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Amortiguador").RequireAuthorization();

        group.MapGet("/", AmortiguadorService.GetAll)
            .WithName("GetAllAmortiguadors")
            .Produces<List<Amortiguador>>();

        group.MapGet("/{id:int}", AmortiguadorService.Find)
            .WithName("GetAmortiguadorById")
            .Produces<Amortiguador>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", AmortiguadorService.Update)
            .WithName("UpdateAmortiguador")
            .Accepts<AmortiguadorDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", AmortiguadorService.Create)
            .WithName("CreateAmortiguador")
            .Accepts<AmortiguadorDto>("application/json")
            .Produces<Amortiguador>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", AmortiguadorService.Delete)
            .WithName("DeleteAmortiguador")
            .Produces(StatusCodes.Status404NotFound);
    }
}