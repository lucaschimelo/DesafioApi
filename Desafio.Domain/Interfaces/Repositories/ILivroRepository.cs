
using Desafio.Domain.DTOs;
using Desafio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.Domain.Interfaces.Repositories
{
    public interface ILivroRepository : IBaseRepository<Livro>
    {
        Task<IEnumerable<LivroFormaComprasDTO>> GetFormaCompraAsync(int id);
        Task<IEnumerable<LivroAutor>> GetLivroAutorAsync(int id);
        Task<IEnumerable<LivroAssunto>> GetLivroAssuntoAsync(int id);
        int Update(Livro livro);
        bool Delete(int codl);
        int Insert(Livro livro);
    }
}
