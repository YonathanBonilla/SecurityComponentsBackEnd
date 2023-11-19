using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using System.Collections.Generic;

namespace EcommerceAuth.service.Users_Rols
{
    public interface IUserRolService
    {
        IEnumerable<User_Rol> TraerUsuariosRoles();

        IEnumerable<User_Rol> TraerUsuariosRoles(int id, int opcion);

        int CrearUsuariosRoles(List<User_Rol> elementos);

        int ActualizarUsuariosRoles(User_Rol user_rol);

        int BorrarUsuariosRoles(User_Rol user_rol);

        IEnumerable<LastAssign> UltimasAsignaciones();
    }
}
