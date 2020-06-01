using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CidadesAuth.Models;

namespace CidadesAuth.Repository
{
    public interface ICidadesRepository
    {
        Task<IEnumerable<Cidade>> GetAll();
        Task<Cidade> GetOne(int codigo);
        Task<int> Add(Cidade cidade);
        Task<int> Update(Cidade cidade);
        Task<int> Delete(int codigo);
        Task<IEnumerable<CidadesPorUf>> GetCidadesPorUF();
        Task<Usuario> GetUsuario(Usuario usuario);
    }
}
