using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> predicate);
        Task<IEnumerable<User>> GetRange(int offset, int count);
        Task<User> GetById(int id);
        Task<User> GetByField(Expression<Func<User, bool>> predicate);
        Task<User> Add(User entity);
        Task<User> Update(User entity);
        Task<int> Delete(int id);
        bool VerifyPasswordHash(string inputPassword, string dbPassword);
    }
}
