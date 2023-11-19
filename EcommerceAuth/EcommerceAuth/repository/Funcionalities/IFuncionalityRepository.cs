using EcommerceAuth.model.entities;
using System.Collections.Generic;

namespace EcommerceAuth.repository.Funcionalities
{
    public interface IFuncionalityRepository
    {
        IEnumerable<Funcionality> GetFuncionalities();

        IEnumerable<Funcionality> GetFuncionality(string nombre);

        int InsertFuncionality(Funcionality funcionality);

        int UpdateFuncionality(Funcionality funcionality);

        int TurnOffFuncionality(int id);

        int TurnOnFuncionality(int id);

        int SearchNameFuncionality(string nombre);
        
        int SearchCodeFuncionality(string code);
    }
}
