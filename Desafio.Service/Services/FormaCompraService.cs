using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Domain.Interfaces.Services;

namespace Desafio.Service.Services
{
    public class FormaCompraService : BaseService<FormaCompra>, IFormaCompraService
    {
        public readonly IFormaCompraRepository _formaCompraRepository;

        public FormaCompraService(IFormaCompraRepository formaCompraRepository)
            : base(formaCompraRepository)
        {
            _formaCompraRepository = formaCompraRepository;
        }
    }
}
