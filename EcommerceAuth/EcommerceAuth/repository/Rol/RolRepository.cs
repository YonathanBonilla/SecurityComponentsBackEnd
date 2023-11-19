using EcommerceAuth.model.context;
using EcommerceAuth.model.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.repository.Rol
{
    public class RolRepository : IRolRepository
    {
        private readonly ModelContext _context;

        private readonly ILogger<RolRepository> _logger;

        public RolRepository(ModelContext context, ILogger<RolRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int ActivarRol(int id)
        {
            try
            {
                Roles rol = _context.Roles.Find(id);

                if (rol == null)
                {
                    return -1;
                }

                rol.Estado = true;

                _context.Update(rol);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("ActivarRol: ", e.Message);
                return -1;
            }
        }

        public int DesactivarRol(int id)
        {
            try
            {
                Roles rol = _context.Roles.Find(id);

                if (rol == null)
                {
                    return -1;
                }

                rol.Estado = false;
                
                _context.Update(rol);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("DesactivarRol: ", e.Message);
                return -1;
            }
        }

        public int GuardaRol(Roles rol)
        {
            try
            {
                Roles consulta = _context.Roles.OrderByDescending(x => x.RolId).FirstOrDefault();

                if (consulta == null)
                    rol.RolId = 1;
                else
                    rol.RolId = consulta.RolId + 1;

                rol.Codigo = Guid.NewGuid().ToString();
                _context.Roles.Add(rol);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("GuardaRol: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<Roles> ListaRoles()
        {
            try
            {
                List<Roles> lista = new();

                lista = (from resp in _context.Roles
                         select new Roles
                         {
                             RolId = resp.RolId,
                             RolName = resp.RolName.Trim(),
                             Codigo = resp.Codigo,
                             Estado = resp.Estado
                         }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                _logger.LogError("ListaRoles: ", e.Message);
                return Enumerable.Empty<Roles>();
            }
        }

        public int SearchRol(string nombre)
        {
            try
            {
                IEnumerable<Roles> lista = from resp in _context.Roles where resp.RolName == nombre select resp;

                return lista.Count();
            }
            catch (Exception e)
            {
                _logger.LogError("SearchRol: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<Roles> TraerRol(string nombre)
        {
            try
            {
                return from rol in _context.Roles where rol.RolName == nombre select rol;
            }
            catch (Exception e)
            {
                _logger.LogError("TraerRol: ", e.Message);
                return null;
            }
        }

        public int UpdateRol(Roles rol)
        {
            try
            {
                _context.Update(rol);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("UpdateRol: ", e.Message);
                return -1;
            }
        }
    }
}
