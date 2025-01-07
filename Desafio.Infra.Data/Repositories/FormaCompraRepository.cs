using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Infra.Data.Context;

namespace Desafio.Infra.Data.Repositories
{
    public class FormaCompraRepository : BaseRepository<FormaCompra>, IFormaCompraRepository
    {
        private readonly DesafioContext _context;        
        public FormaCompraRepository(DesafioContext context)
            : base(context)
        {
            _context = context;            
        }       
    }
}
