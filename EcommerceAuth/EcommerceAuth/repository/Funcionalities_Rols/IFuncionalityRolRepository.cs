using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.repository.Funcionalities_Rols
{
    public interface IFuncionalityRolRepository
    {
        IEnumerable<Funcionality_Rol> GetFuncionalitiesRols();

        IEnumerable<Funcionality_Rol> GetFuncionalityRol(int id);

        IEnumerable<IGrouping<int, FuncRolFull>> GetFuncionalitiesRolsFull();

        IEnumerable<FuncRolFull> GetFuncionalitiesRolsFull(string nombre);

        int InsertFuncionalityRol(List<Funcionality_Rol> elementos);

        int UpdateFuncionalityRol(Funcionality_Rol funcionality_Rol);

        int TurnOffFuncionalityRol(int id);

        int TurnOnFuncionalityRol(int id);

        int DeleteFuncionalityRol(int id);
    }
}
