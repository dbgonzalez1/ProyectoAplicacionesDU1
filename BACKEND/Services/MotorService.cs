using System.Net;
using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;

namespace backend.Services;

using static Validador;

public abstract class MotorService
{
    public static async Task<Motor> Create(MotorDto motor)
    {
        var proveedor = await DataRepository.Proveedor.FindAsync(motor.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        var create = new Motor
        {
            Marca = motor.Marca,
            Modelo = motor.Modelo,
            Potencia = motor.Potencia,
            Torque = motor.Torque,
            Eficiencia = motor.Eficiencia,
            Consumo = motor.Consumo,
            Cilindrada = motor.Cilindrada,
            Proveedor = proveedor
        };

        await DataRepository.Motor.AddAsync(create);
        
        return create;
    }

    public static async Task<Motor> Find(int id)
    {
        var motor = await DataRepository.Motor.FindAsync(id);
        if (motor == null)
        {
            throw new ErrorDeCliente("Motor no encontrado", HttpStatusCode.NotFound);
        }

        return motor;
    }


    public static Task<List<Motor>> GetAll()
    {
        return DataRepository.Motor.ToListAsync();
    }

    public static async Task<IResult> Update(int id, MotorDto motor)
    {
        var encontrado = await Find(id);

        if (encontrado == null)
        {
            throw new ErrorDeCliente("Motor no encontrado", HttpStatusCode.NotFound);
        }

        var proveedor = await DataRepository.Proveedor.FindAsync(motor.IdProveedor);
        
        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }
        
        encontrado.Marca = motor.Marca;
        encontrado.Modelo = motor.Modelo;
        encontrado.Potencia = motor.Potencia;
        encontrado.Torque = motor.Torque;
        encontrado.Eficiencia = motor.Eficiencia;
        encontrado.Consumo = motor.Consumo;
        encontrado.Cilindrada = motor.Cilindrada;
        encontrado.Proveedor = proveedor;
        
        await DataRepository.Motor.UpdateAsync(encontrado);
        return Results.Ok();
    }

    public static async Task Delete(int id)
    {
        var motor = await Find(id);

        if (motor == null)
        {
            throw new ErrorDeCliente("Motor no encontrado", HttpStatusCode.NotFound);
        }

        ValidarRelaciones(await DataRepository.Auto.AnyAsync(el => el.Motor == motor), "autos");
      
        await DataRepository.Motor.RemoveAsync(motor);
    }
}