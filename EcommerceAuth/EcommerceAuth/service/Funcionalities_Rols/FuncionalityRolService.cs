using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using EcommerceAuth.repository.Funcionalities_Rols;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.service.Funcionalities_Rols
{
    public class FuncionalityRolService : IFuncionalityRolService
    {
        private readonly ILogger<FuncionalityRolService> _logger;

        private readonly IFuncionalityRolRepository _repository;

        public FuncionalityRolService(ILogger<FuncionalityRolService> logger, IFuncionalityRolRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public int ActualizarFuncionalidadRol(Funcionality_Rol funcionality_Rol)
        {
            try
            {
                return _repository.UpdateFuncionalityRol(funcionality_Rol);
            }
            catch (Exception e)
            {
                _logger.LogError("ActualizarFuncionalidadRol: ", e.Message);
                return -1;
            }
        }

        public int ApagarFuncionalidadRol(int id)
        {
            try
            {
                return _repository.TurnOffFuncionalityRol(id);
            }
            catch (Exception e)
            {
                _logger.LogError("ApagarFuncionalidadRol: ", e.Message);
                return -1;
            }
        }

        public int BorrarFuncionalidadRol(int id)
        {
            try
            {
                return _repository.DeleteFuncionalityRol(id);
            }
            catch (Exception e)
            {
                _logger.LogError("BorrarFuncionalidadRol: ", e.Message);
                return -1;
            }
        }

        public int CrearFuncionalidadRol(List<Funcionality_Rol> elementos)
        {
            try
            {
                return _repository.InsertFuncionalityRol(elementos);
            }
            catch (Exception e)
            {
                _logger.LogError("CrearFuncionalidadRol: ", e.Message);
                return -1;
            }
        }

        public int EncenderFuncionalidadRol(int id)
        {
            try
            {
                return _repository.TurnOnFuncionalityRol(id);
            }
            catch (Exception e)
            {
                _logger.LogError("EncenderFuncionalidadRol: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<Funcionality_Rol> TraerFuncionalidadesRoles()
        {
            try
            {
                return _repository.GetFuncionalitiesRols();
            }
            catch (Exception e)
            {
                _logger.LogError("TraerFuncionalidadesRoles: ", e.Message);
                return Enumerable.Empty<Funcionality_Rol>();
            }
        }

        public IEnumerable<Funcionality_Rol> TraerFuncionalidadesRoles(int id)
        {
            try
            {
                return _repository.GetFuncionalityRol(id);
            }
            catch (Exception e)
            {
                _logger.LogError("TraerFuncionalidadesRoles: ", e.Message);
                return Enumerable.Empty<Funcionality_Rol>();
            }
        }

        public IEnumerable<IGrouping<int, FuncRolFull>> TraerFuncionalidadesRolesFull()
        {
            try
            {
                return _repository.GetFuncionalitiesRolsFull();
            }
            catch (Exception e)
            {
                _logger.LogError("TraerFuncionalidadesRolesFull: ", e.Message);
                return Enumerable.Empty<IGrouping<int, FuncRolFull>>();
            }
        }

        public IEnumerable<FuncRolFull> TraerFuncionalidadesRolesFull(string nombre)
        {
            try
            {
                return _repository.GetFuncionalitiesRolsFull(nombre);
            }
            catch (Exception e)
            {
                _logger.LogError("TraerFuncionalidadesRolesFull: ", e.Message);
                return Enumerable.Empty<FuncRolFull>();
            }
        }
    }
}
