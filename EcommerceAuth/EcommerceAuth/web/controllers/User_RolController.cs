using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using EcommerceAuth.service.Users_Rols;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAuth.web.controllers
{
    [ApiController]
    [Produces("application/json")]
    //[Route("api/[controller]")]
    public class User_RolController : ControllerBase
    {
        private readonly IUserRolService _userRolService;

        public User_RolController(IUserRolService userRolService)
        {
            _userRolService = userRolService;
        }

        // GET: api/<User_RolController>
        [HttpGet("user_rol/list")]
        public IEnumerable<User_Rol> Get()
        {
            return _userRolService.TraerUsuariosRoles();
        }

        // GET api/<User_RolController>/5
        [HttpGet("user_rol/{id}/{opcion}")]
        public IEnumerable<User_Rol> Get(int id, int opcion)
        {
            return _userRolService.TraerUsuariosRoles(id, opcion);
        }

        // GET api/<User_RolController>/5
        [HttpGet("user_rol/last-assing")]
        public IEnumerable<LastAssign> GetLastAssign()
        {
            return _userRolService.UltimasAsignaciones();
        }

        // POST api/<User_RolController>
        [HttpPost("user_rol/insert")]
        public int Post([FromBody] List<User_Rol> user_rols)
        {
            return _userRolService.CrearUsuariosRoles(user_rols);
        }

        // PUT api/<User_RolController>/5
        [HttpPut("user_rol/update")]
        public int Put([FromBody] User_Rol user_rol)
        {
            return _userRolService.ActualizarUsuariosRoles(user_rol);
        }

        // DELETE api/<User_RolController>/5
        [HttpDelete("user_rol/delete/{id}")]
        public int Delete(User_Rol user_rol)
        {
            return _userRolService.BorrarUsuariosRoles(user_rol);
        }
    }
}
