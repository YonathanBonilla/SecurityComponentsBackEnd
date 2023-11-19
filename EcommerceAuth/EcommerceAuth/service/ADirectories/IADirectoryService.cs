using EcommerceAuth.model.entities;
using System.Collections.Generic;

namespace EcommerceAuth.service.ADirectories
{
    public interface IADirectoryService
    {
        IEnumerable<ADirectory> TraerUsuarios();

        IEnumerable<ADirectory> TraerUsuario(string username);

        int GuardarUsuario(ADirectory aDirectory);

        int ActualizarUsuario(ADirectory aDirectory);
    }
}
