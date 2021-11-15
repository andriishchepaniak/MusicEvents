using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseRepository<TEntity> : 
        IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly AppDbContext _db;
        protected virtual IQueryable<TEntity> ComplexEntities
        {
            get
            {
                return _db.Set<TEntity>();
            }
        }
        public BaseRepository(AppDbContext context)
        {
            _db = context;
        }
        public async virtual Task<TEntity> Add(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public async virtual Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities)
        {
            await _db.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public virtual void Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _db.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual TEntity Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await ComplexEntities.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetRange(int offset, int count)
        {
            return await ComplexEntities
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }

        public void Delete(int id)
        {
            var entity = _db.Set<TEntity>()
                .FirstOrDefault(e => e.Id.Equals(id));
            _db.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetByField(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public void DeleteAll()
        {
            _db.Set<TEntity>().RemoveRange(_db.Set<TEntity>());
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Set<TEntity>().AnyAsync(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().SingleOrDefault(predicate);
        }
    }
}
