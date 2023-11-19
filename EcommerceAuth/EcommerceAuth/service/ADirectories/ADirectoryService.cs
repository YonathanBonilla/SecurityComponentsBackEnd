using EcommerceAuth.model.entities;
using EcommerceAuth.repository.ADirectories;
using EcommerceAuth.service.Funcionalities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EcommerceAuth.service.ADirectories
{
    public class ADirectoryService : IADirectoryService
    {
        private readonly ILogger<FuncionalityService> _logger;

        private readonly IADirectoryRepository _repository;

        public ADirectoryService(ILogger<FuncionalityService> logger, IADirectoryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public int ActualizarUsuario(ADirectory aDirectory)
        {
            try
            {
                return _repository.UpdateUser(aDirectory);
            }
            catch (Exception e)
            {
                _logger.LogError("ActualizarUsuario: ", e.Message);
                return -1;
            }
        }

        public int GuardarUsuario(ADirectory aDirectory)
        {
            try
            {
                return _repository.SaveUser(aDirectory);
            }
            catch (Exception e)
            {
                _logger.LogError("GuardarUsuario: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<ADirectory> TraerUsuario(string username)
        {
            try
            {
                return _repository.GetUser(username);
            }
            catch (Exception e)
            {
                _logger.LogError("TraerUsuario: ", e.Message);
                return null;
            }
        }

        public IEnumerable<ADirectory> TraerUsuarios()
        {
            try
            {
                return _repository.AllUsers();
            }
            catch (Exception e)
            {
                _logger.LogError("TraerUsuarios: ", e.Message);
                return null;
            }
        }
    }
}
