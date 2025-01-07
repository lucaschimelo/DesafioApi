
using Desafio.Domain.Interfaces.Repositories;
using Desafio.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Infra.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DesafioContext _context;      

        public BaseRepository(DesafioContext Context)
        {
            _context = Context;           
        }
        public virtual async Task<TEntity> AddAsync(TEntity obj, CancellationToken ct = default)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(obj, ct);
                await _context.SaveChangesAsync(ct);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual List<TEntity> Get()
        {
            return _context.Set<TEntity>().ToList();
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity obj, CancellationToken ct = default)
        {
            try
            {
                _context.Update(obj);

                await _context.SaveChangesAsync(ct);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual void Remove(TEntity obj)
        {
            try
            {
                _context.Set<TEntity>().Remove(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual void Dispose()
        {
            _context.Dispose();
        }       
    }
}
