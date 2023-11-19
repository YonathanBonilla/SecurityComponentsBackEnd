using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using System.Threading.Tasks;

namespace EcommerceAuth.repository.RefreshToke
{
    public interface IRefreshToken
    {
       bool Save(RefreshTokens newAccessToken);
       Task<RefreshTokens> GetTokenReq(RefreshTokenDto refreshToken);
       Task<bool> RevoqueTokenReq(RefreshTokenDto newAccessToken);
       bool Logout(RefreshTokenDto newAccessToken);
    }
}
