using backend.Data;
using backend.Dto;
using backend.Exceptions;
using backend.Models;
using static backend.Services.Validador;

namespace backend.Services;

public abstract class AmortiguadorService
{
    public static async Task<Amortiguador> Create(AmortiguadorDto auto)
    {
        var proveedor = await DataRepository.Proveedor.FindAsync(auto.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado");
        }

        var create = new Amortiguador
        {
            Marca = auto.Marca,
            Modelo = auto.Modelo,
            Recorrido = auto.Recorrido,
            Fuerza = auto.Fuerza,
            AñosUtil = auto.AñosUtil,
            DiametroDelPiston = auto.DiametroDelPiston,
            Proveedor = proveedor
        };

        await DataRepository.Amortiguador.AddAsync(create);
        return create;
    }

    public static async Task<Amortiguador> Find(int id)
    {
        var amortiguador = await DataRepository.Amortiguador.FindAsync(id);
        if (amortiguador == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado");
        }

        return amortiguador;
    }


    public static Task<List<Amortiguador>> GetAll()
    {
        return DataRepository.Amortiguador.ToListAsync();
    }

    public static async Task<IResult> Update(int id, AmortiguadorDto amortiguador)
    {
        var encontrado = await Find(id);

        encontrado.Marca = amortiguador.Marca;
        encontrado.Modelo = amortiguador.Modelo;
        encontrado.Recorrido = amortiguador.Recorrido;
        encontrado.Fuerza = amortiguador.Fuerza;
        encontrado.AñosUtil = amortiguador.AñosUtil;
        encontrado.DiametroDelPiston = amortiguador.DiametroDelPiston;

        var proveedor = await DataRepository.Proveedor.FindAsync(amortiguador.IdProveedor);

        if (proveedor == null)
        {
            throw new ErrorDeCliente("Proveedor no encontrado");
        }


        encontrado.Proveedor = proveedor;

        await DataRepository.Amortiguador.UpdateAsync(encontrado);
        
        return Results.Ok();
    }

    public static async Task Delete(int id)
    {
        var amortiguador = await Find(id);

        if (amortiguador == null)
        {
            throw new ErrorDeCliente("Amortiguador no encontrado");
        }

        ValidarRelaciones(await DataRepository.Suspension.AnyAsync(el => el.Amortiguador == amortiguador), "suspensiones");
        ValidarRelaciones(await DataRepository.Auto.AnyAsync(el => el.Amortiguador == amortiguador), "autos");

        await DataRepository.Amortiguador.RemoveAsync(amortiguador);

    }
}