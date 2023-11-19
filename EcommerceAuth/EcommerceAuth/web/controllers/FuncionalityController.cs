using EcommerceAuth.model.entities;
using EcommerceAuth.service.Funcionalities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAuth.web.controllers
{
    [ApiController]
    [Produces("application/json")]
    //[Route("api/[controller]")]
    public class FuncionalityController : ControllerBase
    {
        private readonly IFuncionalityService _funcionalityService;

        public FuncionalityController(IFuncionalityService funcionalityService)
        {
            _funcionalityService = funcionalityService;
        }

        // GET: api/<FuncionalityController>
        [HttpGet("funcionality/list")]
        public IEnumerable<Funcionality> Get()
        {
            return _funcionalityService.TraerFuncionalidades();
        }

        // GET api/<FuncionalityController>/5
        [HttpGet("funcionality/{nombre}")]
        public IEnumerable<Funcionality> Get(string nombre)
        {
            return _funcionalityService.TraerFuncionalidad(nombre);
        }

        // GET: api/<FuncionalityController>
        [HttpGet("funcionality/search/name/{nombre}")]
        public int SearchName(string nombre)
        {
            return _funcionalityService.RevisarNombreFuncionalidad(nombre);
        }

        // GET: api/<FuncionalityController>
        [HttpGet("funcionality/search/code/{codigo}")]
        public int SearchCode(string codigo)
        {
            return _funcionalityService.RevisarCodigoFuncionalidad(codigo);
        }

        // POST api/<FuncionalityController>
        [HttpPost("funcionality/insert")]
        public int Post([FromBody] Funcionality funcionality)
        {
            return _funcionalityService.GuardarFuncionalidad(funcionality);
        }

        // PUT api/<FuncionalityController>/5
        [HttpPut("funcionality/update")]
        public int Put([FromBody] Funcionality funcionality)
        {
            return _funcionalityService.ActualizarFuncionalidad(funcionality);
        }

        // DELETE api/<FuncionalityController>/5
        [HttpDelete("funcionality/off/{id}")]
        public int TurnOff(int id)
        {
            return _funcionalityService.ApagarFuncionalidad(id);
        }

        // DELETE api/<FuncionalityController>/5
        [HttpDelete("funcionality/on/{id}")]
        public int TurnOn(int id)
        {
            return _funcionalityService.EncenderFuncionalidad(id);
        }
    }
}
