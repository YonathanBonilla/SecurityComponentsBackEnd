using EcommerceAuth.commons.domains;
using EcommerceAuth.model.context;
using EcommerceAuth.model.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.repository.ADirectories
{
    public class ADirectoryRepository : IADirectoryRepository
    {
        private readonly ModelContext _context;

        private readonly ILogger<ADirectoryRepository> _logger;

        public ADirectoryRepository(ModelContext context, ILogger<ADirectoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public string UserADId(LoginReq loginReq)
        {
            try
            {
                string consulta = _context.ADirectory.
                    Where(x => x.Email.Trim() == loginReq.Email && x.Pwd.Trim() == loginReq.Pwd).
                    Select(x => x.ADId).
                    FirstOrDefault();

                consulta ??= "";

                return consulta;
            }
            catch (Exception e)
            {
                _logger.LogError("UserADId: ", e.Message);
                return "";
            }
        }

        public IEnumerable<ADirectory> AllUsers()
        {
            try
            {
                return from resp in _context.ADirectory select resp;
            }
            catch (Exception e)
            {
                _logger.LogError("AllUsers: ", e.Message);
                return Enumerable.Empty<ADirectory>();
            }
        }

        public IEnumerable<ADirectory> GetUser(string username)
        {
            try
            {
                return from resp in _context.ADirectory where resp.UserName == username select resp;
            }
            catch (Exception e)
            {
                _logger.LogError("GetUser: ", e.Message);
                return null;
            }
        }

        public int SaveUser(ADirectory user)
        {
            try
            {
                Users users = new();

                ADirectory consuDirectory = _context.ADirectory.OrderByDescending(x => x.ADUserId).FirstOrDefault();
                Users consuUsuario = _context.Users.OrderByDescending(x => x.UserId).FirstOrDefault();

                if (consuDirectory == null)
                    user.ADUserId = 1;
                else
                    user.ADUserId = consuDirectory.ADUserId + 1;

                if (consuUsuario == null)
                    users.UserId = 1;
                else
                    users.UserId = consuUsuario.UserId + 1;
                
                var numero = Guid.NewGuid().ToString();

                users.UserADId = numero;

                _context.Users.Add(users);
                _context.SaveChanges();

                user.ADId = numero;
                _context.ADirectory.Add(user);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("SaveUser: ", e.Message);
                return -1;
            }
        }

        public int UpdateUser(ADirectory user)
        {
            try
            {
                _context.Update(user);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("UpdateUser: ", e.Message);
                return -1;
            }
        }
    }
}
