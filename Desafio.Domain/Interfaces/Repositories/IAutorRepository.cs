
using Desafio.Domain.DTOs;
using Desafio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.Domain.Interfaces.Repositories
{
    public interface IAutorRepository : IBaseRepository<Autor>
    {
        Task<IEnumerable<AutorLivroDTO>> GetLivroAutorAsync();
    }
}
