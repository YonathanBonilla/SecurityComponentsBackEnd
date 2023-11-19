using EcommerceAuth.model.entities;
using EcommerceAuth.service.Rol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAuth.web.controllers
{
    [ApiController]
    [Produces("application/json")]
    //[Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        // GET: api/<RolController>
        [HttpGet("rol/list")]
        public IEnumerable<Roles> Get()
        {
            return _rolService.GetRoles();
        }

        // GET api/<RolController>/5
        [HttpGet("rol/{nombre}")]
        public IEnumerable<Roles> Get(string nombre)
        {
            return _rolService.MostrarRol(nombre);
        }

        // GET: api/<RolController>
        [HttpGet("rol/search/{nombre}")]
        public int Search(string nombre)
        {
            return _rolService.RevisarRol(nombre);
        }

        // POST api/<RolController>
        [HttpPost("rol/insert")]
        public int Post([FromBody] Roles rol)
        {
            return _rolService.IngresarRol(rol);
        }

        // PUT api/<RolController>/5
        [HttpPut("rol/update")]
        public int Put([FromBody] Roles rol)
        {
            return _rolService.ActualizarRol(rol);
        }

        // DELETE api/<RolController>/5
        [HttpDelete("rol/off/{id}")]
        public int TurnOff(int id)
        {
            return _rolService.ApagarRol(id);
        }

        // DELETE api/<RolController>/5
        [HttpDelete("rol/on/{id}")]
        public int TurnOn(int id)
        {
            return _rolService.EncenderRol(id);
        }
    }
}
