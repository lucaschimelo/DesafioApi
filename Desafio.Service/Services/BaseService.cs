using Desafio.Domain.Interfaces.Repositories;
using Desafio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Service.Services
{
    public abstract class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;
       
        public BaseService(IBaseRepository<TEntity> Repository)
        {
            _repository = Repository;
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj, CancellationToken ct = default)
        {
            return await _repository.AddAsync(obj, ct);
        }

        public void Dispose()
        {

        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public virtual TEntity Get(int id)
        {
            return _repository.Get(id);
        }

        public virtual List<TEntity> Get()
        {
            return _repository.Get();
        }

        public virtual void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity obj, CancellationToken ct = default)
        {
            return await _repository.UpdateAsync(obj, ct);
        }
    }
}
