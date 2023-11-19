using EcommerceAuth.model.context;
using EcommerceAuth.model.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAuth.repository.Funcionalities
{
    public class FuncionalityRepository : IFuncionalityRepository
    {
        private readonly ModelContext _context;

        private readonly ILogger<FuncionalityRepository> _logger;

        public FuncionalityRepository(ModelContext context, ILogger<FuncionalityRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int TurnOffFuncionality(int id)
        {
            try
            {
                Funcionality fun = _context.Funcionality.Find(id);

                if (fun == null)
                {
                    return -1;
                }

                fun.Estado = false;

                _context.Update(fun);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("TurnOffFuncionality: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<Funcionality> GetFuncionalities()
        {
            try
            {
                List<Funcionality> lista = new();

                lista = (from resp in _context.Funcionality
                         select new Funcionality
                         {
                             FuncionalityId = resp.FuncionalityId,
                             CodTramite = resp.CodTramite,
                             NameFunc = resp.NameFunc.Trim(),
                             Codigo = resp.Codigo,
                             Estado = resp.Estado
                         }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                _logger.LogError("GetFuncionalities: ", e.Message);
                return Enumerable.Empty<Funcionality>();
            }
        }

        public IEnumerable<Funcionality> GetFuncionality(string nombre)
        {
            try
            {
                return from resp in _context.Funcionality where resp.NameFunc == nombre select resp;
            }
            catch (Exception e)
            {
                _logger.LogError("GetFuncionality: ", e.Message);
                return null;
            }
        }

        public int InsertFuncionality(Funcionality funcionality)
        {
            try
            {
                Funcionality consulta = _context.Funcionality.OrderByDescending(x => x.FuncionalityId).FirstOrDefault();

                if (consulta == null)
                    funcionality.FuncionalityId = 1;
                else
                    funcionality.FuncionalityId = consulta.FuncionalityId + 1;

                funcionality.Codigo = Guid.NewGuid().ToString();
                _context.Funcionality.Add(funcionality);
                _context.SaveChanges();

                return funcionality.FuncionalityId;
            }
            catch (Exception e)
            {
                _logger.LogError("InsertFuncionality: ", e.Message);
                return -1;
            }
        }

        public int UpdateFuncionality(Funcionality funcionality)
        {
            try
            {
                _context.Update(funcionality);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("UpdateFuncionality: ", e.Message);
                return -1;
            }
        }

        public int TurnOnFuncionality(int id)
        {
            try
            {
                Funcionality fun = _context.Funcionality.Find(id);

                if (fun == null)
                {
                    return -1;
                }

                fun.Estado = true;

                _context.Update(fun);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("TurnOnFuncionality: ", e.Message);
                return -1;
            }
        }

        public int SearchNameFuncionality(string nombre)
        {
            try
            {
                IEnumerable<Funcionality> lista = from resp in _context.Funcionality where resp.NameFunc == nombre select resp;

                return lista.Count();
            }
            catch (Exception e)
            {
                _logger.LogError("SearchNameFuncionality: ", e.Message);
                return -1;
            }
        }

        public int SearchCodeFuncionality(string code)
        {
            try
            {
                IEnumerable<Funcionality> lista = from resp in _context.Funcionality where resp.CodTramite == code select resp;

                return lista.Count();
            }
            catch (Exception e)
            {
                _logger.LogError("SearchCodeFuncionality: ", e.Message);
                return -1;
            }
        }
    }
}
