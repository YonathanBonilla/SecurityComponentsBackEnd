using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using EcommerceAuth.service.Funcionalities_Rols;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAuth.web.controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class Funcionality_RolController : ControllerBase
    {
        private readonly IFuncionalityRolService _funcionalityRolService;

        public Funcionality_RolController(IFuncionalityRolService funcionalityRolService)
        {
            _funcionalityRolService = funcionalityRolService;
        }

        // GET: api/<Funcionality_RolController>
        [HttpGet("funcionality_rol/list")]
        public IEnumerable<Funcionality_Rol> Get()
        {
            return _funcionalityRolService.TraerFuncionalidadesRoles();
        }

        // GET api/<Funcionality_RolController>/5
        [HttpGet("funcionality_rol/list/func")]
        public IEnumerable<Funcionality_Rol> Get(int id)
        {
            return _funcionalityRolService.TraerFuncionalidadesRoles(id);
        }

        // GET: api/<Funcionality_RolController>
        [HttpGet("funcionality_rol/list/full")]
        public IEnumerable<IGrouping<int, FuncRolFull>> GetFull()
        {
            return _funcionalityRolService.TraerFuncionalidadesRolesFull();
        }

        // GET: api/<Funcionality_RolController>
        [HttpGet("funcionality_rol/list/full/{nombre}")]
        public IEnumerable<FuncRolFull> GetFull(string nombre)
        {
            return _funcionalityRolService.TraerFuncionalidadesRolesFull(nombre);
        }

        // POST api/<Funcionality_RolController>
        [HttpPost("funcionality_rol/insert")]
        public int Post([FromBody] List<Funcionality_Rol> funcionality_Rols)
        {
            return _funcionalityRolService.CrearFuncionalidadRol(funcionality_Rols);
        }

        // PUT api/<Funcionality_RolController>/5
        [HttpPut("funcionality_rol/update")]
        public int Put([FromBody] Funcionality_Rol funcionality_Rols)
        {
            return _funcionalityRolService.ActualizarFuncionalidadRol(funcionality_Rols);
        }

        // DELETE api/<Funcionality_RolController>/5
        [HttpDelete("funcionality_rol/off/{id}")]
        public int TurnOff(int id)
        {
            return _funcionalityRolService.ApagarFuncionalidadRol(id);
        }

        // DELETE api/<Funcionality_RolController>/5
        [HttpDelete("funcionality_rol/on/{id}")]
        public int TurnOn(int id)
        {
            return _funcionalityRolService.EncenderFuncionalidadRol(id);
        }

        // DELETE api/<Funcionality_RolController>/5
        [HttpDelete("funcionality_rol/delete/{id}")]
        public int Delete(int id)
        {
            return _funcionalityRolService.BorrarFuncionalidadRol(id);
        }
    }
}
