using System.Net;
using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;

namespace backend.Services;

public abstract class AutoService
{
    public static async Task<Auto> Create(AutoDto auto)
    {
        var marca = await DataRepository.MarcaAuto.FindAsync(auto.IdMarca);
        var amortiguador = await DataRepository.Amortiguador.FindAsync(auto.IdAmortiguador);
        var transmision = await DataRepository.Transmision.FindAsync(auto.IdTransmision);
        var suspension = await DataRepository.Suspension.FindAsync(auto.IdSuspension);
        var motor = await DataRepository.Motor.FindAsync(auto.IdMotor);
        var frenos = await DataRepository.Frenos.FindAsync(auto.IdFrenos);
        var neumatico = await DataRepository.Neumatico.FindAsync(auto.IdNeumatico);

        if (marca == null)
        {
            throw new ErrorDeCliente("Marca no encontrada", HttpStatusCode.NotFound);
        }

        if (amortiguador == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado", HttpStatusCode.NotFound);
        }

        if (transmision == null)
        {
            throw new ErrorDeCliente("Transmision no encontrada", HttpStatusCode.NotFound);
        }

        if (suspension == null)
        {
            throw new ErrorDeCliente("Suspension no encontrada", HttpStatusCode.NotFound);
        }

        if (motor == null)
        {
            throw new ErrorDeCliente("Motor no encontrado", HttpStatusCode.NotFound);
        }

        if (frenos == null)
        {
            throw new ErrorDeCliente("Frenos no encontrados", HttpStatusCode.NotFound);
        }

        if (neumatico == null)
        {
            throw new ErrorDeCliente("Neumatico no encontrado", HttpStatusCode.NotFound);
        }

        var creado = new Auto
        {
            Amortiguador = amortiguador,
            Frenos = frenos,
            Marca = marca,
            Motor = motor,
            Suspension = suspension,
            Transmision = transmision,
            Neumatico = neumatico,
            Modelo = auto.Modelo
        };

        await DataRepository.Auto.AddAsync(creado);
        return creado;
    }

    public static async Task<Auto> Find(int id)
    {
        var auto = await DataRepository.Auto.FindAsync(id);
        if (auto == null)
        {
            throw new ErrorDeCliente("Auto no encontrado");
        }

        return auto;
    }


    public static Task<List<Auto>> GetAll()
    {
        return DataRepository.Auto.ToListAsync();
    }

    public static async Task<IResult> Update(int id, AutoDto auto)
    {
        var encontrado = await Find(id);

        var marca = await DataRepository.MarcaAuto.FindAsync(auto.IdMarca);
        var amortiguador = await DataRepository.Amortiguador.FindAsync(auto.IdAmortiguador);
        var transmision = await DataRepository.Transmision.FindAsync(auto.IdTransmision);
        var suspension = await DataRepository.Suspension.FindAsync(auto.IdSuspension);
        var motor = await DataRepository.Motor.FindAsync(auto.IdMotor);
        var frenos = await DataRepository.Frenos.FindAsync(auto.IdFrenos);
        var neumatico = await DataRepository.Neumatico.FindAsync(auto.IdNeumatico);

        if (encontrado == null)
        {
            throw new ErrorDeCliente("Auto no encontrado", HttpStatusCode.NotFound);
        }

        if (marca == null)
        {
            throw new ErrorDeCliente("Marca no encontrada", HttpStatusCode.NotFound);
        }

        if (amortiguador == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado", HttpStatusCode.NotFound);
        }

        if (transmision == null)
        {
            throw new ErrorDeCliente("Transmision no encontrada", HttpStatusCode.NotFound);
        }

        if (suspension == null)
        {
            throw new ErrorDeCliente("Suspension no encontrada", HttpStatusCode.NotFound);
        }

        if (motor == null)
        {
            throw new ErrorDeCliente("Motor no encontrado", HttpStatusCode.NotFound);
        }

        if (frenos == null)
        {
            throw new ErrorDeCliente("Frenos no encontrados", HttpStatusCode.NotFound);
        }

        if (neumatico == null)
        {
            throw new ErrorDeCliente("Neumatico no encontrado", HttpStatusCode.NotFound);
        }

        encontrado.Marca = marca;
        encontrado.Amortiguador = amortiguador;
        encontrado.Transmision = transmision;
        encontrado.Suspension = suspension;
        encontrado.Motor = motor;
        encontrado.Frenos = frenos;
        encontrado.Neumatico = neumatico;
        encontrado.Modelo = auto.Modelo;

        await DataRepository.Auto.UpdateAsync(encontrado);
        return Results.Ok();
        
    }

    public static async Task Delete(int id)
    {
        var auto = await Find(id);

        if (auto == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado");
        }

        await DataRepository.Auto.RemoveAsync(auto);
    }
}