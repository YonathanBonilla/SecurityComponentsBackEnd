using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using System.Collections.Generic;

namespace EcommerceAuth.repository.ADirectories
{
    public interface IADirectoryRepository
    {
        string UserADId(LoginReq loginReq);

        IEnumerable<ADirectory> AllUsers();

        IEnumerable<ADirectory> GetUser(string username);

        int SaveUser(ADirectory user);

        int UpdateUser(ADirectory user);
    }
}
