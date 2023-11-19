using EcommerceAuth.commons.domains;
using EcommerceAuth.model.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.repository.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ModelContext _context;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(ModelContext context, ILogger<AuthRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public model.entities.ADirectory ADInfo(string id)
        {
            return _context.ADirectory.Where(x => x.ADId == id).FirstOrDefault();
        }

        public List<AuthRolesInfo> RolsByUser(string id)
        {
            try
            {
                List<AuthRolesInfo> list = new List<AuthRolesInfo>();

                var query = _context.User_Rol.
                            Include(u => u.Users).
                            Include(r => r.Roles).
                            Include(y => y.Funcionality).
                            Where(x=> x.Users.UserADId.Trim() == id).
                            ToArrayAsync().Result;

                foreach (var foo in query)
                {
                    list.Add(new AuthRolesInfo { 
                        UserADId = foo.Users.UserADId.Trim(),
                        RolName = foo.Roles.RolName.Trim(),
                        NameFunc = foo.Funcionality.NameFunc.Trim()
                    });
                }

                return list;
            }
            catch(Exception e)
            {
                _logger.LogError("RolsByUser: ", e.Message);
                return null;
            }
        }

        public List<UserDto> UsersRoles(string rol)
        {
            try
            {
                List<UserDto> list = new List<UserDto>();

                var ADId = _context.User_Rol.
                            Include(u => u.Users).
                            Include(r => r.Roles).
                            Where(r => r.Roles.RolName.Trim().ToLower() == rol).
                            Select(p => p.Users.UserADId).
                            ToList();
                
                foreach (var foo in ADId)
                {
                    var query = _context.ADirectory.
                            Where(x => x.ADId == foo.Trim()).FirstOrDefault(); 

                    if(query != null)
                        list.Add(new UserDto
                        {
                            Name = query.FirtsName.Trim(),
                            SurName = query.LastName.Trim(),
                            UserId = query.ADId.Trim(),
                            UserName = query.UserName != null ? query.UserName.Trim(): ""
                        });
                }

                return list;
            }
            catch (Exception e)
            {
                _logger.LogError("UsersRoles: ", e.Message);
                return null;
            }
        }
    }
}
