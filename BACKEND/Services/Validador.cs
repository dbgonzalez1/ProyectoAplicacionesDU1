
using backend.Exceptions;

namespace backend.Services;

public abstract class Validador
{
    public static void ValidarRelaciones(bool tieneRelaciones, string relacion)
    {
        if (tieneRelaciones)
        {
            throw new ErrorDeCliente($"No se puede eliminar la entidad porque tiene {relacion} asociados");
        }
    }
}