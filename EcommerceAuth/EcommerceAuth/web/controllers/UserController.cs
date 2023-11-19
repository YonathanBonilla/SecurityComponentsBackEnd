using EcommerceAuth.model.entities;
using EcommerceAuth.service.ADirectories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAuth.web.controllers
{
    [ApiController]
    [Produces("application/json")]
    //[Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IADirectoryService _aDirectoryService;

        public UserController(IADirectoryService aDirectoryService)
        {
            _aDirectoryService = aDirectoryService;
        }

        // GET: api/<UserController>
        [HttpGet("user/list")]
        public IEnumerable<ADirectory> Get()
        {
            return _aDirectoryService.TraerUsuarios();
        }

        // GET api/<UserController>/5
        [HttpGet("user/{nombre}")]
        public IEnumerable<ADirectory> Get(string nombre)
        {
            return _aDirectoryService.TraerUsuario(nombre);
        }

        [HttpPost("user/insert")]
        public int Post([FromBody] ADirectory aDirectory)
        {
            return _aDirectoryService.GuardarUsuario(aDirectory);
        }

        [HttpPut("user/update")]
        public int Put([FromBody] ADirectory aDirectory)
        {
            return _aDirectoryService.ActualizarUsuario(aDirectory);
        }
    }
}
