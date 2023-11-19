using EcommerceAuth.model.entities;
using System.Collections.Generic;

namespace EcommerceAuth.service.Funcionalities
{
    public interface IFuncionalityService
    {
        IEnumerable<Funcionality> TraerFuncionalidades();

        IEnumerable<Funcionality> TraerFuncionalidad(string nombre);

        int GuardarFuncionalidad(Funcionality funcionality);

        int ActualizarFuncionalidad(Funcionality funcionality);

        int ApagarFuncionalidad(int id);

        int EncenderFuncionalidad(int id);

        int RevisarNombreFuncionalidad(string nombre);

        int RevisarCodigoFuncionalidad(string codigo);
    }
}
