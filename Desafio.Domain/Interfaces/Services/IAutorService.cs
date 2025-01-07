using Desafio.Domain.DTOs;
using Desafio.Domain.Entities;
using System.Threading.Tasks;

namespace Desafio.Domain.Interfaces.Services
{
    public interface IAutorService : IBaseService<Autor>
    {
        Task<ReportAutorLivroDTO> ReportAutorLivroAsync();
    }
}
