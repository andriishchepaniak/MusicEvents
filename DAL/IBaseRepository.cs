using Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetRange(int offset, int count);
        Task<TEntity> GetById(int id);
        Task<TEntity> Add(TEntity entity);
        Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
    }
}
