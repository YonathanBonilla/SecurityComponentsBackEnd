using EcommerceAuth.model.entities;
using System.Collections.Generic;

namespace EcommerceAuth.repository.Rol
{
    public interface IRolRepository
    {
        IEnumerable<Roles> ListaRoles();

        IEnumerable<Roles> TraerRol(string nombre);
        
        int GuardaRol(Roles rol);

        int UpdateRol(Roles rol);

        int DesactivarRol(int id);

        int ActivarRol(int id);

        int SearchRol(string nombre);
    }
}
