using Desafio.Domain.Entities;

namespace Desafio.Domain.Interfaces.Services
{
    public interface ILivroService : IBaseService<Livro>
    {
        int Update(Livro livro);
        bool Delete(int codl);
        int Insert(Livro livro);
    }
}
