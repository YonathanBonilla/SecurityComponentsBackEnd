using EcommerceAuth.commons.domains;
using EcommerceAuth.model.context;
using EcommerceAuth.model.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.repository.Funcionalities_Rols
{
    public class FuncionalityRolRepository : IFuncionalityRolRepository
    {
        private readonly ModelContext _context;

        private readonly ILogger<FuncionalityRolRepository> _logger;

        public FuncionalityRolRepository(ModelContext context, ILogger<FuncionalityRolRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int DeleteFuncionalityRol(int id)
        {
            try
            {
                int contador = 0, respuesta;
                IEnumerable<Funcionality_Rol> lista = from resp in _context.Funcionality_Rol where 
                                                      resp.FuncionalityId == id select resp;

                var datos = lista.ToList();

                foreach(var item in datos)
                {
                    _context.Remove(item);
                    respuesta = _context.SaveChanges();
                    contador += respuesta;
                }

                return contador;
            }
            catch (Exception e)
            {
                _logger.LogError("DeleteFuncionalityRol: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<Funcionality_Rol> GetFuncionalitiesRols()
        {
            try
            {
                List<Funcionality_Rol> list = new();

                list = (from resp in _context.Funcionality_Rol
                        select new Funcionality_Rol
                        {
                            Funcionality_RolId = resp.Funcionality_RolId,
                            FuncionalityId = resp.FuncionalityId,
                            Funcionality = resp.Funcionality,
                            RolId = resp.RolId,
                            Roles = resp.Roles,
                            Estado = resp.Estado
                        }).ToList();

                return list;
            }
            catch (Exception e)
            {
                _logger.LogError("GetFuncionalitiesRols: ", e.Message);
                return Enumerable.Empty<Funcionality_Rol>();
            }
        }

        public IEnumerable<IGrouping<int, FuncRolFull>> GetFuncionalitiesRolsFull()
        {
            try
            {
                List<FuncRolFull> lista = new();

                lista = (from resp in _context.Funcionality_Rol
                         select new FuncRolFull 
                         {
                             FuncionalityId = resp.FuncionalityId,
                             Codigo = resp.Funcionality.Codigo,
                             CodFunc = resp.Funcionality.CodTramite,
                             NameFunc = resp.Funcionality.NameFunc.Trim(),
                             RolId = resp.RolId,
                             RolName = resp.Roles.RolName.Trim(),
                             State = resp.Estado
                         }).OrderBy(x => x.FuncionalityId).ToList();

                var grupos = lista.GroupBy(x => x.FuncionalityId);
                
                return grupos;
            }
            catch (Exception e)
            {
                _logger.LogError("GetFuncionalitiesRolsFull: ", e.Message);
                return Enumerable.Empty<IGrouping<int, FuncRolFull>>();
            }
        }

        public IEnumerable<FuncRolFull> GetFuncionalitiesRolsFull(string nombre)
        {
            try
            {
                List<FuncRolFull> lista = new();

                lista = (from resp in _context.Funcionality_Rol where resp.Funcionality.NameFunc == nombre
                         select new FuncRolFull
                         {
                             FuncionalityId = resp.FuncionalityId,
                             Codigo = resp.Funcionality.Codigo,
                             CodFunc = resp.Funcionality.CodTramite,
                             NameFunc = resp.Funcionality.NameFunc.Trim(),
                             RolId = resp.RolId,
                             RolName = resp.Roles.RolName.Trim(),
                             State = resp.Estado
                         }).OrderBy(x => x.FuncionalityId).ToList();

                return lista;
            }
            catch (Exception e)
            {
                _logger.LogError("GetFuncionalitiesRolsFull: ", e.Message);
                return Enumerable.Empty<FuncRolFull>();
            }
        }

        public IEnumerable<Funcionality_Rol> GetFuncionalityRol(int id)
        {
            try
            {
                List<Funcionality_Rol> list = new();

                list = (from resp in _context.Funcionality_Rol
                        where resp.FuncionalityId == id
                        select new Funcionality_Rol
                        {
                            Funcionality_RolId = resp.Funcionality_RolId,
                            FuncionalityId = resp.FuncionalityId,
                            Funcionality = resp.Funcionality,
                            RolId = resp.RolId,
                            Roles = resp.Roles,
                            Estado = resp.Estado
                        }).ToList();

                return list;
            }
            catch (Exception e)
            {
                _logger.LogError("GetFuncionalities: ", e.Message);
                return Enumerable.Empty<Funcionality_Rol>();
            }
        }

        public int InsertFuncionalityRol(List<Funcionality_Rol> elementos)
        {
            int contador = 0, respuesta;

            try
            {
                foreach (var item in elementos)
                {
                    _context.Funcionality_Rol.Add(item);
                    respuesta = _context.SaveChanges();
                    contador += respuesta;
                }

                return contador;
            }
            catch (Exception e)
            {
                _logger.LogError("InsertFuncionalityRol: ", e.Message);
                return -1;
            }
        }

        public int TurnOffFuncionalityRol(int id)
        {
            try
            {
                IEnumerable<Funcionality_Rol> consulta = from resp in _context.Funcionality_Rol where resp.FuncionalityId == id select resp;

                var temporal = consulta.ToList();

                if(!consulta.Any()) 
                {
                    return -1;
                }

                foreach(var item in temporal)
                {
                    item.Estado = false;
                    _context.Update(item);
                    _context.SaveChanges();
                }

                return 1;
            }
            catch (Exception e)
            {
                _logger.LogError("TurnOffFuncionalityRol: ", e.Message);
                return -1;
            }
        }

        public int TurnOnFuncionalityRol(int id)
        {
            try
            {
                IEnumerable<Funcionality_Rol> consulta = from resp in _context.Funcionality_Rol where resp.FuncionalityId == id select resp;

                var temporal = consulta.ToList();

                if (!consulta.Any())
                {
                    return -1;
                }

                foreach (var item in temporal)
                {
                    item.Estado = true;
                    _context.Update(item);
                    _context.SaveChanges();
                }

                return 1;
            }
            catch (Exception e)
            {
                _logger.LogError("TurnOnFuncionalityRol: ", e.Message);
                return -1;
            }
        }

        public int UpdateFuncionalityRol(Funcionality_Rol funcionality_Rol)
        {
            try
            {
                _context.Update(funcionality_Rol);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("UpdateFuncionalityRol: ", e.Message);
                return -1;
            }
        }
    }
}
