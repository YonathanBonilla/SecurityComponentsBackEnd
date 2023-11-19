using EcommerceAuth.model.entities;
using EcommerceAuth.repository.Rol;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EcommerceAuth.service.Rol
{
    public class RolService : IRolService
    {
        private readonly ILogger<RolService> _logger;
        
        private readonly IRolRepository _repository;

        public RolService(ILogger<RolService> logger, IRolRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public int ActualizarRol(Roles rol)
        {
            try
            {
                return _repository.UpdateRol(rol);
            }
            catch (Exception e)
            {
                _logger.LogError("ActualizarRol: ", e.Message);
                return -1;
            }
        }

        public int ApagarRol(int id)
        {
            try
            {
                return _repository.DesactivarRol(id);
            }
            catch (Exception e)
            {
                _logger.LogError("ApagarRol: ", e.Message);
                return -1;
            }
        }

        public int EncenderRol(int id)
        {
            try
            {
                return _repository.ActivarRol(id);
            }
            catch (Exception e)
            {
                _logger.LogError("EncenderRol: ", e.Message);
                return -1;
            }
        }

        public int IngresarRol(Roles rol)
        {
            try
            {
                return _repository.GuardaRol(rol);
            }
            catch (Exception e)
            {
                _logger.LogError("IngresarRol: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<Roles> MostrarRol(string nombre)
        {
            try
            {
                return _repository.TraerRol(nombre);
            }
            catch (Exception e)
            {
                _logger.LogError("MostrarRol: ", e.Message);
                return null;
            }
        }

        public int RevisarRol(string nombre)
        {
            try
            {
                return _repository.SearchRol(nombre);
            }
            catch (Exception e)
            {
                _logger.LogError("RevisarRol: ", e.Message);
                return -1;
            }
        }

        IEnumerable<Roles> IRolService.GetRoles()
        {
			try
			{
                return _repository.ListaRoles();
			}
            catch (Exception e)
            {
                _logger.LogError("GetRoles: ", e.Message);
                return null;
            }
        }
    }
}
