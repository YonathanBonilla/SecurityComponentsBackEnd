using EcommerceAuth.model.entities;
using System.Collections.Generic;

namespace EcommerceAuth.service.Rol
{
    public interface IRolService
    {
        IEnumerable<Roles> GetRoles();

        IEnumerable<Roles> MostrarRol(string nombre);

        int IngresarRol(Roles rol);

        int ActualizarRol(Roles rol);

        int ApagarRol(int id);

        int EncenderRol(int id);

        int RevisarRol(string nombre);
    }
}
