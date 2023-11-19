using EcommerceAuth.commons.domains;
using EcommerceAuth.model.entities;
using EcommerceAuth.repository.Users_Rols;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.service.Users_Rols
{
    public class UserRolService : IUserRolService
    {
        private readonly ILogger<UserRolService> _logger;

        private readonly IUserRolRepository _repository;

        public UserRolService(ILogger<UserRolService> logger, IUserRolRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public int ActualizarUsuariosRoles(User_Rol user_rol)
        {
            try
            {
                return _repository.UpdateUsersRols(user_rol);
            }
            catch (Exception e)
            {
                _logger.LogError("ActualizarUsuariosRoles: ", e.Message);
                return -1;
            }
        }

        public int BorrarUsuariosRoles(User_Rol user_rol)
        {
            try
            {
                return _repository.DeleteUsersRols(user_rol);
            }
            catch (Exception e)
            {
                _logger.LogError("BorrarUsuariosRoles: ", e.Message);
                return -1;
            }
        }

        public int CrearUsuariosRoles(List<User_Rol> elementos)
        {
            try
            {
                return _repository.InsertUsersRols(elementos);
            }
            catch (Exception e)
            {
                _logger.LogError("CrearUsuariosRoles: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<User_Rol> TraerUsuariosRoles(int id, int opcion)
        {
            try
            {
                if (opcion == 1)
                {
                    return _repository.GetUsers(id);
                }
                else if (opcion == 2)
                {
                    return _repository.GetRols(id);
                }
                else if (opcion == 3)
                {
                    return _repository.GetFuncionalities(id);
                }
                else
                {
                    return Enumerable.Empty<User_Rol>();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("TraerUsuarios: ", e.Message);
                return Enumerable.Empty<User_Rol>();
            }
        }

        public IEnumerable<User_Rol> TraerUsuariosRoles()
        {
            try
            {
                return _repository.GetUsersRols();
            }
            catch (Exception e)
            {
                _logger.LogError("TraerUsuariosRoles: ", e.Message);
                return Enumerable.Empty<User_Rol>();
            }
        }

        public IEnumerable<LastAssign> UltimasAsignaciones()
        {
            try
            {
                return _repository.LastAssignments();
            }
            catch (Exception e)
            {
                _logger.LogError("UltimasAsignaciones: ", e.Message);
                return Enumerable.Empty<LastAssign>();
            }
        }
    }
}
