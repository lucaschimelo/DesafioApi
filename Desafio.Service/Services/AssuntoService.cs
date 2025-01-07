using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Domain.Interfaces.Services;

namespace Desafio.Service.Services
{
    public class AssuntoService : BaseService<Assunto>, IAssuntoService
    {
        public readonly IAssuntoRepository _assuntoRepository;

        public AssuntoService(IAssuntoRepository assuntoRepository)
            : base(assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }
    }
}
