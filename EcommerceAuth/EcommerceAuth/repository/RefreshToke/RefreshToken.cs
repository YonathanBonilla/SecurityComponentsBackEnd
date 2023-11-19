using EcommerceAuth.commons.domains;
using EcommerceAuth.model.context;
using EcommerceAuth.model.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAuth.repository.RefreshToke
{
    public class RefreshToken : IRefreshToken
    {
        private readonly ModelContext _context;
        private readonly ILogger<RefreshToken> _logger;

        public RefreshToken(ModelContext context, ILogger<RefreshToken> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Save(RefreshTokens newAccessToken)
        {
            try
            {
                RefreshTokens consulta = _context.RefreshTokens.OrderByDescending(x => x.RefreshTokenId).FirstOrDefault();

                if (consulta == null)
                    newAccessToken.RefreshTokenId = 1;
                else
                    newAccessToken.RefreshTokenId = consulta.RefreshTokenId + 1;

                _context.RefreshTokens.Add(newAccessToken);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("RolsByUser: ", e.Message);
                throw;
            }


        }

        public  Task<RefreshTokens> GetTokenReq(RefreshTokenDto refreshToken)
        {
            try
            {
                return _context.RefreshTokens.Where(x => x.TokenValue.Trim() == refreshToken.TokenValue
                                    && x.UserID.Trim() == refreshToken.UserID).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("GetTokenReq: ", e.Message);
                throw;
            }
        }

        public async Task<bool> RevoqueTokenReq(RefreshTokenDto refreshToken)
        {
            try
            {
                var cancelRefTokens = await _context.RefreshTokens.Where(x => x.IsActive == true
                                        && x.UserID.Trim() == refreshToken.UserID && x.IsUsed == false).ToListAsync();

                foreach (var foo in cancelRefTokens)
                {
                    foo.IsUsed = true;
                    foo.IsActive = false;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("RevoqueTokenReq: ", e.Message);
                throw;
            }
        }

        public bool Logout(RefreshTokenDto refreshToken)
        {
            try
            {
                var cancelRefTokens =  _context.RefreshTokens.Where(x => x.UserID.Trim() == refreshToken.UserID
                           && x.TokenValue == refreshToken.TokenValue).ToList();

                foreach (var foo in cancelRefTokens)
                {
                    foo.IsUsed = true;
                    foo.IsActive = false;
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Logout: ", e.Message);
                throw;
            }
        }
    }
}
