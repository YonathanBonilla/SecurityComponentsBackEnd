using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using System.Collections.Generic;

namespace EcommerceAuth.repository.Users_Rols
{
    public interface IUserRolRepository
    {
        IEnumerable<User_Rol> GetUsersRols();

        IEnumerable<User_Rol> GetUsers(int id);

        IEnumerable<User_Rol> GetRols(int id);

        IEnumerable<User_Rol> GetFuncionalities(int id);

        int InsertUsersRols(List<User_Rol> elementos);

        int UpdateUsersRols(User_Rol user_rol);

        int DeleteUsersRols(User_Rol user_rol);

        IEnumerable<LastAssign> LastAssignments();
    }
}
