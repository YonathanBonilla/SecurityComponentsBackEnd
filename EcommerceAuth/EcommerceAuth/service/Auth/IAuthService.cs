using EcommerceAuth.commons.domains;
using System.Collections.Generic;

namespace EcommerceAuth.service.Auth
{
    public interface IAuthService
    {
        TokenInfo GenerateTokenReq(LoginReq loginReq);
        TokenInfo GetTokenReq(RefreshTokenDto refreshToken);
        bool Logout(RefreshTokenDto refreshToken);
        List<UserDto> UsersRoles(ClientIdReq request, string rol);
        TokenMs usersTokenAsync(ClientIdReq clientId);
    }
}
