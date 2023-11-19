using EcommerceAuth.commons.domains;
using System.Collections.Generic;

namespace EcommerceAuth.repository.Auth
{
    public interface IAuthRepository
    {
        List<AuthRolesInfo> RolsByUser(string email);
        model.entities.ADirectory ADInfo(string id);
        List<UserDto> UsersRoles(string request);
    }
}
