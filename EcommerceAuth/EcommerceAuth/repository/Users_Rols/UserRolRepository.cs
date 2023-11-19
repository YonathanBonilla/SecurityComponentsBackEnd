using EcommerceAuth.commons.domains;
using EcommerceAuth.model.context;
using EcommerceAuth.model.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace EcommerceAuth.repository.Users_Rols
{
    public class UserRolRepository : IUserRolRepository
    {
        private readonly ModelContext _context;

        private readonly ILogger<UserRolRepository> _logger;

        public UserRolRepository(ModelContext context, ILogger<UserRolRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int DeleteUsersRols(User_Rol user_rol)
        {
            try
            {
                _context.User_Rol.Remove(user_rol);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("DeleteUsersRols: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<User_Rol> GetFuncionalities(int id)
        {
            try
            {
                List<User_Rol> users = new();

                users = (from dato in _context.User_Rol where dato.FuncionalityId == id
                         select new User_Rol
                         {
                             User_RolId = dato.RolId,
                             RolId = dato.RolId,
                             Roles = dato.Roles,
                             FuncionalityId = dato.FuncionalityId,
                             Funcionality = dato.Funcionality,
                             UserId = dato.UserId,
                             Users = dato.Users
                         }).ToList();

                return users;
            }
            catch (Exception e)
            {
                _logger.LogError("GetFuncionalities: ", e.Message);
                return Enumerable.Empty<User_Rol>();
            }
        }

        public IEnumerable<User_Rol> GetRols(int id)
        {
            try
            {
                List<User_Rol> users = new();

                users = (from dato in _context.User_Rol where dato.RolId == id
                         select new User_Rol
                         {
                             User_RolId = dato.RolId,
                             RolId = dato.RolId,
                             Roles = dato.Roles,
                             FuncionalityId = dato.FuncionalityId,
                             Funcionality = dato.Funcionality,
                             UserId = dato.UserId,
                             Users = dato.Users
                         }).ToList();

                return users;
            }
            catch (Exception e)
            {
                _logger.LogError("GetRols: ", e.Message);
                return Enumerable.Empty<User_Rol>();
            }
        }

        public IEnumerable<User_Rol> GetUsers(int id)
        {
            try
            {
                List<User_Rol> users = new();

                users = (from dato in _context.User_Rol where dato.UserId == id
                         select new User_Rol
                         {
                             User_RolId = dato.RolId,
                             RolId = dato.RolId,
                             Roles = dato.Roles,
                             FuncionalityId = dato.FuncionalityId,
                             Funcionality = dato.Funcionality,
                             UserId = dato.UserId,
                             Users = dato.Users
                         }).ToList();

                return users;
            }
            catch (Exception e)
            {
                _logger.LogError("GetUsers: ", e.Message);
                return Enumerable.Empty<User_Rol>();
            }
        }

        public IEnumerable<User_Rol> GetUsersRols()
        {
            try
            {
                List<User_Rol> list = new();

                list = (from dato in _context.User_Rol select new User_Rol
                {
                    User_RolId = dato.RolId,
                    RolId = dato.RolId,
                    Roles = dato.Roles,
                    FuncionalityId = dato.FuncionalityId,
                    Funcionality = dato.Funcionality,
                    UserId = dato.UserId,
                    Users = dato.Users
                }).ToList();

                return list;
            }
            catch (Exception e)
            {
                _logger.LogError("GetUsersRols: ", e.Message);
                return Enumerable.Empty<User_Rol>();
            }
        }

        public int InsertUsersRols(List<User_Rol> elementos)
        {
            int contador = 0, respuesta;
            User_Rol consulta;

            try
            {
                foreach (var item in elementos)
                {
                    consulta = _context.User_Rol.OrderByDescending(x => x.User_RolId).FirstOrDefault();

                    if (consulta == null)
                        item.User_RolId = 1;
                    else
                        item.User_RolId = consulta.User_RolId + 1;

                    _context.User_Rol.Add(item);
                    respuesta = _context.SaveChanges();
                    contador += respuesta;
                }

                return contador;
            }
            catch (Exception e)
            {
                _logger.LogError("InsertUsersRols: ", e.Message);
                return contador;
            }
        }

        public IEnumerable<LastAssign> LastAssignments()
        {
            try
            {
                List<LastAssign> list = new();

                list = (from ur in _context.User_Rol
                        join us in _context.Users on ur.UserId equals us.UserId
                        join r in _context.Roles on ur.RolId equals r.RolId
                        join f in _context.Funcionality on ur.FuncionalityId equals f.FuncionalityId
                        join ad in _context.ADirectory on us.UserADId equals ad.ADId
                        select new LastAssign
                        {
                            UserRolId = ur.User_RolId,
                            UserId = us.UserId,
                            UserADId = us.UserADId,
                            UserName = ad.UserName,
                            FuncionalityId = f.FuncionalityId,
                            CodFunc = f.CodTramite,
                            NameFunc = f.NameFunc,
                            RolId = r.RolId,
                            RolName = r.RolName
                        }).OrderByDescending(or => or.UserRolId).ToList();

                return list;
            }
            catch (Exception e)
            {
                _logger.LogError("LastAssignments: ", e.Message);
                return Enumerable.Empty<LastAssign>();
            }
        }

        public int UpdateUsersRols(User_Rol user_rol)
        {
            try
            {
                _context.Update(user_rol);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("UpdateUsersRols: ", e.Message);
                return -1;
            }
        }
    }
}
