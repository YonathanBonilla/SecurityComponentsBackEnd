using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.service.Funcionalities_Rols
{
    public interface IFuncionalityRolService
    {
        IEnumerable<Funcionality_Rol> TraerFuncionalidadesRoles();

        IEnumerable<Funcionality_Rol> TraerFuncionalidadesRoles(int id);

        IEnumerable<IGrouping<int, FuncRolFull>> TraerFuncionalidadesRolesFull();

        IEnumerable<FuncRolFull> TraerFuncionalidadesRolesFull(string nombre);

        int CrearFuncionalidadRol(List<Funcionality_Rol> elementos);

        int ActualizarFuncionalidadRol(Funcionality_Rol funcionality_Rol);

        int ApagarFuncionalidadRol(int id);

        int EncenderFuncionalidadRol(int id);

        int BorrarFuncionalidadRol(int id);
    }
}
