using System.Net;
using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;

namespace backend.Services;

using static Validador;

public abstract class NeumaticoService
{
    public static async Task<Neumatico> Create(NeumaticoDto auto)
    {
        var proveedor = await DataRepository.Proveedor.FindAsync(auto.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        var create = new Neumatico
        {
            Marca = auto.Marca,
            Modelo = auto.Modelo,
            Relacion = auto.Relacion,
            Eficiencia = auto.Eficiencia,
            Consumo = auto.Consumo,
            NumeroDeMarchas = auto.NumeroDeMarchas,
            Proveedor = proveedor
        };

        await DataRepository.Neumatico.AddAsync(create);
        return create;
        
    }

    public static async Task<Neumatico> Find(int id)
    {
        var neumatico = await DataRepository.Neumatico.FindAsync(id);
        if (neumatico == null)
        {
            throw new ErrorDeCliente("Neumatico no encontrado", HttpStatusCode.NotFound);
        }

        return neumatico;
    }


    public static Task<List<Neumatico>> GetAll()
    {
        return DataRepository.Neumatico.ToListAsync();
    }

    public static async Task<IResult> Update(int id, NeumaticoDto neumatico)
    {
        var encontrado = await Find(id);

        if (encontrado == null)
        {
            throw new ErrorDeCliente("Neumatico no encontrado", HttpStatusCode.NotFound);
        }

        var proveedor = await DataRepository.Proveedor.FindAsync(neumatico.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado", HttpStatusCode.NotFound);
        }

        encontrado.Marca = neumatico.Marca;
        encontrado.Modelo = neumatico.Modelo;
        encontrado.Relacion = neumatico.Relacion;
        encontrado.Eficiencia = neumatico.Eficiencia;
        encontrado.Consumo = neumatico.Consumo;
        encontrado.NumeroDeMarchas = neumatico.NumeroDeMarchas;
        encontrado.Proveedor = proveedor;

        await DataRepository.Neumatico.UpdateAsync(encontrado);
        
        return Results.Ok();
    }

    public static async Task Delete(int id)
    {
        var neumatico = await Find(id);

        if (neumatico == null)
        {
            throw new ErrorDeCliente("Motor no encontrado", HttpStatusCode.NotFound);
        }

        ValidarRelaciones(await DataRepository.Auto.AnyAsync(el => el.Neumatico == neumatico), "autos");

        await DataRepository.Neumatico.RemoveAsync(neumatico);
    }
}