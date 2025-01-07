using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Domain.Interfaces.Services;

namespace Desafio.Service.Services
{
    public class LivroService : BaseService<Livro>, ILivroService
    {
        public readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
            : base(livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public int Update(Livro livro)
        {
            return _livroRepository.Update(livro);
        }
        public bool Delete(int codl)
        {
            return _livroRepository.Delete(codl);
        }
        public int Insert(Livro livro)
        {
            return _livroRepository.Insert(livro);
        }
    }
}
