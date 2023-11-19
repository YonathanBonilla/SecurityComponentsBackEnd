using EcommerceAuth.model.entities;
using EcommerceAuth.repository.Funcionalities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EcommerceAuth.service.Funcionalities
{
    public class FuncionalityService : IFuncionalityService
    {
        private readonly ILogger<FuncionalityService> _logger;

        private readonly IFuncionalityRepository _repository;

        public FuncionalityService(ILogger<FuncionalityService> logger, IFuncionalityRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public int ActualizarFuncionalidad(Funcionality funcionality)
        {
            try
            {
                return _repository.UpdateFuncionality(funcionality);
            }
            catch (Exception e)
            {
                _logger.LogError("ActualizarFuncionalidad: ", e.Message);
                return -1;
            }
        }

        public int ApagarFuncionalidad(int id)
        {
            try
            {
                return _repository.TurnOffFuncionality(id);
            }
            catch (Exception e)
            {
                _logger.LogError("ApagarFuncionalidad: ", e.Message);
                return -1;
            }
        }

        public int EncenderFuncionalidad(int id)
        {
            try
            {
                return _repository.TurnOnFuncionality(id);
            }
            catch (Exception e)
            {
                _logger.LogError("EncenderFuncionalidad: ", e.Message);
                return -1;
            }
        }

        public int GuardarFuncionalidad(Funcionality funcionality)
        {
            try
            {
                return _repository.InsertFuncionality(funcionality);
            }
            catch (Exception e)
            {
                _logger.LogError("GuardarFuncionalidad: ", e.Message);
                return -1;
            }
        }

        public int RevisarCodigoFuncionalidad(string codigo)
        {
            try
            {
                return _repository.SearchCodeFuncionality(codigo);
            }
            catch (Exception e)
            {
                _logger.LogError("RevisarCodigoFuncionalidad: ", e.Message);
                return -1;
            }
        }

        public int RevisarNombreFuncionalidad(string nombre)
        {
            try
            {
                return _repository.SearchNameFuncionality(nombre);
            }
            catch (Exception e)
            {
                _logger.LogError("RevisarNombreFuncionalidad: ", e.Message);
                return -1;
            }
        }

        public IEnumerable<Funcionality> TraerFuncionalidad(string nombre)
        {
            try
            {
                return _repository.GetFuncionality(nombre);
            }
            catch (Exception e)
            {
                _logger.LogError("TraerFuncionalidad: ", e.Message);
                return null;
            }
        }

        public IEnumerable<Funcionality> TraerFuncionalidades()
        {
            try
            {
                return _repository.GetFuncionalities();
            }
            catch (Exception e)
            {
                _logger.LogError("TraerFuncionalidades: ", e.Message);
                return null;
            }
        }
    }
}
