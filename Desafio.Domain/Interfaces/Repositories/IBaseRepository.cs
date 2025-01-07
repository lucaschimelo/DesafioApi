
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity obj, CancellationToken ct = default);

        Task<TEntity> GetAsync(int id);

        Task<List<TEntity>> GetAsync();

        TEntity Get(int id);

        List<TEntity> Get();

        Task<TEntity> UpdateAsync(TEntity obj, CancellationToken ct = default);

        void Remove(TEntity obj);

        void Dispose();

    }
}
