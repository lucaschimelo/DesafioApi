using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Infra.Data.Context;

namespace Desafio.Infra.Data.Repositories
{
    public class AssuntoRepository : BaseRepository<Assunto>, IAssuntoRepository
    {
        private readonly DesafioContext _context;        
        public AssuntoRepository(DesafioContext context)
            : base(context)
        {
            _context = context;            
        }       
    }
}
