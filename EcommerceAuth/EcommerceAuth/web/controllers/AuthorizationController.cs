using EcommerceAuth.commons.domains;
using EcommerceAuth.service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAuth.web.controllers
{
    [ApiController]
    [Produces("application/json")]
  //  [Route("/v2/")]
    public class AuthorizationController : ControllerBase
    {        
        private IAuthService _authService;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("auth/login")]
        public IActionResult login(LoginReq loginReq)
        {
            if (loginReq == null)
                BadRequest();

            var token = _authService.GenerateTokenReq(loginReq);
    
            if (token == null)
               return Unauthorized();
            
            return Ok(token);
        }

        [HttpPost("auth/refresh-token")]
        public IActionResult refreshToken(RefreshTokenDto refreshToken)
        {
            if (refreshToken == null)
                BadRequest();

            var RefreshToken = _authService.GetTokenReq(refreshToken);

            if (RefreshToken == null)
                return Unauthorized();

            return Ok(RefreshToken);
        }

        [HttpPost("auth/logout")]
        public IActionResult logout(RefreshTokenDto refreshToken)
        {
            if (refreshToken == null)
                BadRequest();

            if (!_authService.Logout(refreshToken))
                return Unauthorized();

            return Ok();
        }

        [HttpPost("auth/users-by-role/{rol}")]
        public IActionResult usersRoles(ClientIdReq request, [System.Web.Http.FromUri] string rol)
        {
            if (request == null)
                BadRequest();

            var usersRoles = _authService.UsersRoles(request, rol.ToLower());

            if (usersRoles == null)
                return Unauthorized();

            return Ok(usersRoles);
        }

        [HttpPost("auth/users-token")]
        public IActionResult usersToken(ClientIdReq clientId)
        {
            if (clientId == null)
                BadRequest();

            var usersRoles = _authService.usersTokenAsync(clientId);

            if (usersRoles == null)
                return Unauthorized();

            return Ok(usersRoles);
        }
    }
}
