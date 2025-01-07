using Dapper;
using Desafio.Domain.DTOs;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Infra.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.Infra.Data.Repositories
{
    public class AutorRepository : BaseRepository<Autor>, IAutorRepository
    {
        private readonly DesafioContext _context;        
        public AutorRepository(DesafioContext context)
            : base(context)
        {
            _context = context;            
        }       

        public async Task<IEnumerable<AutorLivroDTO>> GetLivroAutorAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                return await connection.QueryAsync<AutorLivroDTO>("SELECT * FROM Vi_AutorLivro ORDER BY Codigo ASC;");
            }
        }
    }
}
