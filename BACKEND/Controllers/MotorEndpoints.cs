using backend.Dto;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

public static class MotorEndpoints
{
    public static void MapMotorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/Motor").RequireAuthorization();

        group.MapGet("/", MotorService.GetAll)
            .WithName("GetAllMotors")
            .Produces<List<Motor>>();

        group.MapGet("/{id:int}", MotorService.Find)
            .WithName("GetMotorById")
            .Produces<Motor>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:int}", MotorService.Update)
            .WithName("UpdateMotor")
            .Accepts<MotorDto>("application/json")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", MotorService.Create)
            .WithName("CreateMotor")
            .Accepts<MotorDto>("application/json")
            .Produces<Motor>(StatusCodes.Status201Created);

        group.MapDelete("/{id:int}", MotorService.Delete)
            .WithName("DeleteMotor")
            .Produces<Motor>()
            .Produces(StatusCodes.Status404NotFound);
    }
}