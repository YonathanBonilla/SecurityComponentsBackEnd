using EcommerceAuth.commons.domains;
using EcommerceAuth.commons.utils;
using EcommerceAuth.model.entities;
using EcommerceAuth.repository.ADirectories;
using EcommerceAuth.repository.Auth;
using EcommerceAuth.repository.RefreshToke;
using EcommerceAuth.service.CookieSec;
using EcommerceAuth.web.controllers;
using IdentityServer4;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EcommerceAuth.service.Auth
{
    public class AuthService : IAuthService
    {
        public IConfiguration _configuration { get; }

        private readonly IAuthRepository _authrepo;

        private readonly IADirectoryRepository _authADrepo;

        private readonly IRefreshToken _refreshToken;

        private readonly IKeyVaultUtils _secretClient;

        private readonly ILogger<AuthService> _logger;

        private readonly ICookieSec _cookieSec;


        private List<AuthRolesInfo> listRol;

        public AuthService(IConfiguration configuration,
                           IAuthRepository authrepo,
                           IADirectoryRepository authADrepo,
                           IKeyVaultUtils secretClient,
                           ILogger<AuthService> logger,
                           IRefreshToken refreshToken,
                           ICookieSec cookieSec)
        {
            _configuration = configuration;
            _authrepo = authrepo;
            _authADrepo = authADrepo;
            _refreshToken = refreshToken;
            _secretClient = secretClient;
            _logger = logger;
            _cookieSec = cookieSec;
        }

        private string GenerateToken()
        {
            try
            {
                ClaimsIdentity claimsIdentity = AddClaims();

                var issuerToken = _configuration.GetSection("TokenParams:ISSUER").Value;
                var expireTime = _configuration.GetSection("TokenParams:EXPIRE_MINUTES").Value;
                IdentityModelEventSource.ShowPII = true;

                #region debug
                //var securityKey = File.ReadAllBytes(@"C:\Users\John\keys\2048AuthEpid2\k\privatekey.xml");
                //var strSecurityKey = Encoding.UTF8.GetString(securityKey);
                #endregion

                #region prod
                var strSecurityKey = "<RSAKeyValue><Modulus>s9xFbAWWDrIA9918TWMYLWFjCXyaWwi6OBCjZzra8kTX/xuJW0RhhrMW+rcUANGEZC7435adZjy9hwitLWMr84FkTVrwnUg3/TJcbK1YVPD+OtnMRYZdC8h1rBJ6JpmuroANMoG/etTMj9nQT1wp5/p/qL09T/7fYvk+OXBqRiGWPEVCHsYV+zirS4ZUIRcATz9pcc+XNI4z5c9F+zL2aRFRuR4Jn1vU1dLh3Gt/OAO7zvyUX8YhUvOqh0CQQHUR+UIHfsoOnDfYBEeCMiHhsh0Wn9UsATDunN9XObnyggcAhg+EJwbSxHye/h1+3RJ0xbcr3t0MAdIKfN4hhSORZw==</Modulus><Exponent>AQAB</Exponent><P>z6OBlIiipqHeeVfTBwn3kiHBpXCBzAwzidAxsllUTs1vdKYv3fn/Liw+wuDgQGAmZmZaSD+98yhlcwKIWSnwDOUX4zjQoO6EcsEQ3LCWbkxzJEDDZS4LTVM0dsBfkYtYftgFF1QzS8efgtzzujXgcyVeVBw5tRUjRq9NVCUbYYk=</P><Q>3cB7M9hAJX06BRCRNEyg2g6paIFyahiH5ysgmmLXjjiJqcQp8TSOWojpEVq9Fg2rbKaMEBibroWigSQI0S1IRpp4AuTgK5wqXwVcjf19sa4dbW1pO5cQTiyROJXYVr/HZUMdlnOZ1vxv3keI/aTuvHrHdm0e/WHUz2FwYk4TT28=</Q><DP>tvLJ6aW6KjfVXfUT6s/NYuR7Stmmg7L8diEKqKosroI0AgOriVoMjRJO+kZJUG9nZjIouh/vrZ+aQ4unt2hMKhBwy7PzntiJutKBmPG+mdCiYv7tKuk27Bqzzy4RBpxQjPKpCbUWvV5WHWexGBEAgnQaeOGltve47T7oU4ueZIE=</DP><DQ>suGpwRnKLTPGkP1bVQgXna/EIwSBSmq9ftKAe2oOnrCnBGa0utn9l7Tn5lL/Q4IFbiEvzXA8bd1pSKnL3wmbSzEibJs65pMUbxCBKB17vtBI9tQS1ro2sgkroKzoFpqf++TXQl3AlPeQOowUSyA4YZzduH+wRxAbPqtIoFSBKUs=</DQ><InverseQ>jNO6pc1ZHKX9skpxCwDuQcJ1xmrJqNraWcxRkavlAT+qcRnA7qRYNLZP8jXSTPD9a4ND6EijzSYs9Sa8lwPbZlNq3MqXHT35YPGx9NkAFYN5q7XvJFiQTZA2dKyDsOPZ8vHERl4JpAWDkzbTA3O7K9TRJBb/zM2X67Wahw6bBu8=</InverseQ><D>AGE98Hk8i/BmBx/7q0SI5k9FqWjgXgg3ATGUh34ljiNWz3ESIMiI3o6659SOJx+P+PiObNFekXUd1kM3SsccVybnfKSzHXtgJ59rpqQD/T3vasog39ArbDpEG4lVi1gkvuQN4xS8+kmRlTYxDTTDqypYCsRqdMdinJKsF4+ZcdvVHuvQoI0sj83zl2h7tV/0Pjg+UiLCysMUx3voifCslHXzfLKwk6JScr8IR+1AQ0HIi49MjW3TL7uSEkLWwGWT8fM28uxSO6Bt9P2TJoC+7062QbYI6fidvXTbXoE58GMbv00nYjQCk7ztJI8caZN+KGRhhwQ1UAWfXO+kaMq1SQ==</D></RSAKeyValue>";//_secretClient.GetSecret("JwtPrivateKeyLoginV2").Result.ToString();
                #endregion

                using RSA rsa = RSA.Create();
                rsa.FromXmlString(strSecurityKey);

                var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
                {
                    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                };

                var token = new JwtSecurityToken(
                   issuerToken,
                   "AudienceNotDefined",
                   claimsIdentity.Claims,
                   expires: DateTime.Now.AddMinutes(Convert.ToInt32(expireTime)),
                   signingCredentials: signingCredentials);
                var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return jwtTokenString;
            }
            catch (Exception e)
            {
                _logger.LogError("GenerateToken: ", e.Message);
                return e.InnerException.ToString();
            }
        }

        private void cookie()
        {
            try
            {
                _cookieSec.CreateCookie<CookieModel>(".cookhouse", new CookieModel
                {
                    Name = "John",
                    UserId = 123
                });
            }
            catch
            { }

        }

        private ClaimsIdentity AddClaims()
        {
            var adInfo = _authrepo.ADInfo(listRol[0].UserADId);
            var claims02 = new List<Claim>();
            var claims01 = new List<Claim>();
            claims02.Add(new Claim("ver", "1.0"));
            claims02.Add(new Claim("sub", listRol[0].UserADId));
            claims02.Add(new Claim("id", listRol[0].UserADId));
            claims02.Add(new Claim("email", adInfo.Email.Trim()));
            claims02.Add(new Claim("name", adInfo.FirtsName == null ? "" : adInfo.FirtsName.Trim()));
            claims02.Add(new Claim("surname", adInfo.LastName == null ? "" : adInfo.LastName.Trim()));
            claims02.Add(new Claim("username", adInfo.UserName == null ? "" : adInfo.UserName.Trim()));

            List<DtoTramite> dto = new List<DtoTramite>();
            foreach (var rols in listRol.Select(x => x.NameFunc).Distinct())
            {

                List<RolesDto> dtostrr = new List<RolesDto>();
                foreach (var rol in listRol.Where(x => x.NameFunc == rols))
                {
                    dtostrr.Add(new RolesDto() { Rol = rol.RolName });
                }

                dto.Add(new DtoTramite
                {
                    ProcedureName = rols,
                    RolesDto = dtostrr
                });
            }

            string jsonString = JsonConvert.SerializeObject(dto);

            claims02.Add(new Claim("access", jsonString, IdentityServerConstants.ClaimValueTypes.Json));

            return new ClaimsIdentity(
               claims02.ToList()
            );
        }

        public class DtoTramite
        {
            public string ProcedureName { get; set; }
            public List<RolesDto> RolesDto { get; set; }
        }

        public class RolesDto
        {
            public string Rol { get; set; }
        }


        public TokenInfo GenerateTokenReq(LoginReq loginReq)
        {
            try
            {
                string adId = ValidateActiveDirectory(loginReq).Trim();

                if (string.IsNullOrEmpty(adId))
                    return null;

                listRol = _authrepo.RolsByUser(adId);
                if (listRol.Count == 0)
                    return null;

                string token = GenerateToken();

                var RefreshToken = GenerateRefreshToken(adId);

                return GenerateTokenInfo(adId, token, RefreshToken);
            }
            catch (Exception e)
            {
                _logger.LogError("GenerateTokenReq: ", e.Message);
                return null;
            }
        }

        public TokenInfo GetTokenReq(RefreshTokenDto refreshToken)
        {
            try
            {
                var refreshTokenValue = _refreshToken.GetTokenReq(refreshToken).Result;
                if (refreshTokenValue is null || refreshTokenValue.IsActive == false
                    || refreshTokenValue.DateExpired <= DateTime.UtcNow)
                {
                    return null;
                }

                // security block
                if (refreshTokenValue.IsUsed)
                {
                    _refreshToken.RevoqueTokenReq(refreshToken);
                    return null;
                }

                refreshTokenValue.IsUsed = true;

                listRol = _authrepo.RolsByUser(refreshTokenValue.UserID);
                if (listRol.Count == 0)
                    return null;

                var newAccessToken = GenerateToken();

                if (string.IsNullOrEmpty(newAccessToken))
                    return null;

                var newRefreshToken = GenerateRefreshToken(refreshTokenValue.UserID);

                return GenerateTokenInfo(refreshTokenValue.UserID, newAccessToken, newRefreshToken);
            }
            catch (Exception e)
            {
                _logger.LogError("GetTokenReq: ", e.Message);
                return null;
            }
        }

        private TokenInfo GenerateTokenInfo(string adId, string accessToken, RefreshTokenDto refreshToken)
        {
            var expireTime = Convert.ToInt32(_configuration.GetSection("TokenParams:EXPIRE_MINUTES").Value);
            var refreshExpireTime = Convert.ToInt32(_configuration.GetSection("TokenParams:REFRESH_EXPIRE_MINUTES").Value);

            cookie();

            return new TokenInfo
            {
                UserId = adId.Trim(),
                //Sub = Guid.NewGuid().ToString(),
                TokenType = "bearer",
                AccessToken = accessToken,
                RefreshToken = refreshToken.TokenValue,
                Exp = DateToLong(DateTime.UtcNow.AddMinutes(expireTime)),
                Nbf = DateToLong(DateTime.UtcNow),
                ExpRt = DateToLong(DateTime.UtcNow.AddMinutes(refreshExpireTime)),
                AccessTokenExpiresIn = Convert.ToInt32(expireTime) * 60,
                RefreshTokenExpiresIn = Convert.ToInt32(refreshExpireTime) * 60
            };
        }

        private RefreshTokenDto GenerateRefreshToken(string userId)
        {
            try
            {
                var refreshExpireTime = _configuration.GetSection("TokenParams:REFRESH_EXPIRE_MINUTES").Value;
                var newAccessToken = new RefreshTokens
                {
                    IsActive = true,
                    IsUsed = false,
                    TokenValue = Guid.NewGuid().ToString("N"),
                    DateIssued = DateTime.UtcNow,
                    DateExpired = DateTime.UtcNow.AddMinutes(Convert.ToInt32(refreshExpireTime)),
                    UserID = userId.Trim()
                };

                _refreshToken.Save(newAccessToken);

                return new RefreshTokenDto
                {
                    TokenValue = newAccessToken.TokenValue,
                    UserID = newAccessToken.UserID,
                    DateIssued = newAccessToken.DateIssued,
                    DateExpired = newAccessToken.DateExpired
                };

            }
            catch (Exception e)
            {
                _logger.LogError("GenerateRefreshToken: ", e.Message);
                return null;
            }
        }

        private string ValidateActiveDirectory(LoginReq loginReq)
        {
            ////////////////////////////////////
            //// go to active directory conexion
            ////////////////////////////////////

            try
            {                
                return _authADrepo.UserADId(loginReq);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool Logout(RefreshTokenDto refreshToken)
        {
            try
            {
                return _refreshToken.Logout(refreshToken);
            }
            catch (Exception e)
            {
                _logger.LogError("Logout: ", e.Message);
                return false;
            }
        }

        public List<UserDto> UsersRoles(ClientIdReq request, string rol) {
            try
            {
                if (request.ClientId != _configuration.GetSection("clientsId:usersByRol").Value)
                    return null;

                return _authrepo.UsersRoles(rol);
            }
            catch (Exception e)
            {
                _logger.LogError("UsersRoles: ", e.Message);
                return null;
            }
        }

        private long DateToLong(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time = date.ToUniversalTime().Subtract(epoch);
            return time.Ticks / TimeSpan.TicksPerSecond;
        }

        public TokenMs usersTokenAsync(ClientIdReq clientId)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/soaintje.onmicrosoft.com/oauth2/v2.0/token");

            request.Content = new StringContent("grant_type=client_credentials" + "&" +
                                               "client_id=" + clientId.ClientId + "&" +
                                               "client_secret=ODw8Q~ZzFNgX0W_2beWKxZjGqwetqVLdgJUmYaJN" + "&" +
                                               "scope=https://graph.microsoft.com/.default");

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<TokenMs>(response.Content.ReadAsStringAsync().Result);
        }
    }
}

